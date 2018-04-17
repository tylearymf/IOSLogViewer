using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOSLogViewer
{
    public partial class IOSLogViewer : Form
    {
        #region 字段
        readonly string[] mMonths = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
        Process mProcess;
        LogInfo mLastLog;
        object mLockInvoke = new object();
        object mLockUpdateView = new object();
        object mLockInitView = new object();
        string mPrefixName;
        #endregion

        #region 属性
        /// <summary>
        /// 默认自动选中最新log
        /// </summary>
        public bool isScrollBottom { private set; get; } = true;
        public List<LogInfo> allLogInfos { private set; get; }
        public List<LogInfo> showingLogInfos { private set; get; }
        public bool isChangeText { private set; get; } = true;
        public int rowIndex { private set; get; }
        #endregion

        #region 构造方法 初始化
        public IOSLogViewer()
        {
            InitializeComponent();

            KillAllLogProcess();
            InitView();

            if (logView.Columns.Count > 0)
            {
                logView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                logView.Columns[0].MinimumWidth = logView.Width - 3;
            }
            logView.RowHeadersVisible = false;
            logView.MultiSelect = false;
            logView.RowTemplate.MinimumHeight = logView.RowTemplate.Height = 30;
            logView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            logView.CellStateChanged += LogView_RowStateChanged;
        }

        void InitView()
        {
            lock (mLockInitView)
            {
                allLogInfos = new List<LogInfo>();
                logView.Rows.Clear();
                LogDetail.Text = string.Empty;
                mLastLog = null;

                logView.Columns.Add("日志", "日志");
                logView.Columns.Add("数据", string.Empty);
                IsFollowLast.Checked = isScrollBottom;
            }
        }
        #endregion

        #region 其它事件
        void LogView_RowStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            if (showingLogInfos == null || e.Cell.RowIndex < 0 || e.Cell.RowIndex >= showingLogInfos.Count) return;
            LogDetail.Text = showingLogInfos[e.Cell.RowIndex].fullLog;
        }

        void ConditionText_TextChanged(object sender, EventArgs e)
        {
            isChangeText = true;
        }
        #endregion

        #region 按钮点击
        void IsFollowLast_Click(object sender, EventArgs e)
        {
            isScrollBottom = !isScrollBottom;
            IsFollowLast.Checked = isScrollBottom;
        }

        void StartBtn_Click(object pSender, EventArgs pEvent)
        {
            StartReceive();
        }
        void StopBtn_Click(object sender, EventArgs e)
        {
            UpdateView();
        }

        void ClearBtn_Click(object sender, EventArgs e)
        {
            InitView();
        }
        #endregion

        #region 主要逻辑
        void UpdateView()
        {
            isChangeText = false;
            Invoke(new EventHandler(delegate
            {
                lock (mLockUpdateView)
                {
                    try
                    {
                        showingLogInfos = new List<LogInfo>();
                        logView.Rows.Clear();
                        LogDetail.Text = string.Empty;

                        var tText = ConditionText.Text;
                        var tLogInfos = new List<LogInfo>();
                        var tTempLogInfos = new List<LogInfo>(allLogInfos);
                        foreach (var tLogInfo in tTempLogInfos)
                        {
                            if (tLogInfo.Contains(tText))
                            {
                                tLogInfos.Add(tLogInfo);
                            }
                        }

                        var tIsScrollBottom = isScrollBottom;
                        isScrollBottom = false;
                        foreach (var tLogInfo in tLogInfos)
                        {
                            AddLogInfoToLast(tLogInfo);
                        }
                        isScrollBottom = tIsScrollBottom;

                        SelectLastRow();
                    }
                    catch { }
                }
            }));
        }

        /// <summary>
        /// 加一条log到最后
        /// </summary>
        void AddLogInfoToLast(LogInfo pInfo, bool pIsSelectLastRow = false)
        {
            if (pInfo == null) return;

            showingLogInfos.Add(pInfo);
            if (pIsSelectLastRow)
            {
                Invoke(new EventHandler(delegate
                {
                    lock (mLockInvoke)
                    {
                        AddInfo(pInfo);
                        SelectLastRow();
                    }
                }));
            }
            else
            {
                AddInfo(pInfo);
            }
        }

        void AddInfo(LogInfo pInfo)
        {
            var tRowIndex = logView.Rows.Add();
            logView.Rows[tRowIndex].Cells[0].Value = pInfo.shortLog;
            logView.Rows[tRowIndex].Cells[1].Value = pInfo;
            logView.Rows[tRowIndex].Cells[0].Style.BackColor = rowIndex++ % 2 == 0 ? Color.White : DefaultBackColor;
        }

        void SelectLastRow()
        {
            if (isScrollBottom)
            {
                logView.ClearSelection();
                if (logView.RowCount > 0)
                {
                    var tRowIndex = logView.RowCount - 1;
                    logView.FirstDisplayedScrollingRowIndex = tRowIndex;
                    logView.Rows[tRowIndex].Selected = true;
                    logView.Rows[tRowIndex].Cells[0].Selected = true;
                }
            }
        }
        #endregion

        #region 获取日志逻辑
        /// <summary>
        /// 开始接收
        /// </summary>
        void StartReceive()
        {
            if (mProcess != null)
            {
                CancelReceive();
                return;
            }
            StartBtn.Text = "停止";
            Control.CheckForIllegalCrossThreadCalls = false;
            mProcess = new Process();
            mProcess.StartInfo.FileName = "cmd.exe";
            mProcess.StartInfo.WorkingDirectory = ".";
            mProcess.StartInfo.UseShellExecute = false;
            mProcess.StartInfo.RedirectStandardInput = true;
            mProcess.StartInfo.RedirectStandardOutput = true;
            mProcess.StartInfo.CreateNoWindow = true;
            mProcess.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            mProcess.Start();

            mProcess.StandardInput.WriteLine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libimobiledevice/idevicesyslog.exe"));
            InitView();
            mProcess.BeginOutputReadLine();
        }

        /// <summary>
        /// 取消接收
        /// </summary>
        void CancelReceive()
        {
            StartBtn.Text = "开始";
            if (mProcess != null)
            {
                mProcess.Kill();
                mProcess = null;
            }
            KillAllLogProcess();
        }

        /// <summary>
        /// 处理接收到的Log
        /// </summary>
        /// <param name="pSender"></param>
        /// <param name="pMessage"></param>
        void OutputHandler(object pSender, DataReceivedEventArgs pMessage)
        {
            if (string.IsNullOrEmpty(pMessage.Data)) return;
            LogInfo tLog = null;
            if (string.IsNullOrEmpty(mPrefixName) && pMessage.Data.IndexOf(' ') == 3 && mMonths.Contains(pMessage.Data.Substring(0, 3).ToUpper()))
            {
                mPrefixName = pMessage.Data.Substring(0, 3);
            }
            if (mLastLog == null || (!string.IsNullOrEmpty(mPrefixName) && pMessage.Data.StartsWith(mPrefixName)))
            {
                tLog = new LogInfo();
                tLog.AddLog(pMessage.Data, false);
                allLogInfos.Add(tLog);
                mLastLog = tLog;

                if (isChangeText) UpdateView();
                else if (tLog.Contains(ConditionText.Text))
                {
                    AddLogInfoToLast(tLog, true);
                }
            }
            else
            {
                mLastLog.AddLog(pMessage.Data);
                tLog = mLastLog;

                if (isScrollBottom)
                {
                    SelectLastRow();
                }
            }
        }
        #endregion

        #region 释放资源
        void KillAllLogProcess()
        {
            var tSyslogs = Process.GetProcessesByName("idevicesyslog");
            if (tSyslogs != null) tSyslogs.ToList().ForEach(x => x.Kill());
        }

        protected override void OnClosed(EventArgs e)
        {
            CancelReceive();
        }
        #endregion
    }
}
