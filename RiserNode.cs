using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiFilling
{
    public enum RiserTaskStatus
    {
        None,
        DataEntered,
        OperStarting,
        FillStarting,
        Filling,
        OperEnding,
        FillEnding,
        FilledOk,
        FilledFault
    }

    public enum RiserFetchMode
    {
        ReadFirstNineRegisters,
        ReadDeepWorkLevels,
        ReadForTrends,
        ReadFullMap
    }

    public class RiserNode : IFetchingParams
    {
        private int _lastLevel;
        //private readonly List<int> _queue = new List<int>();
        private ushort[] _hregs = new ushort[]
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
        private ushort[] _oldregs = new ushort[]
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
        private int _stopCount;
        private DateTime _lastFething = DateTime.MinValue;
        private bool _active;
        public int Index { get; set; }
        public string Name { get; set; }
        public int Node { get; set; }
        public int Channel { get; set; }
        public int Overpass { get; set; }
        public int Way { get; set; }
        public string Product { get; set; }
        public int Riser { get; set; }
        public int NodeType { get; set; }
        public RiserAddress Address
        {
            get
            {
                return new RiserAddress
                    {
                        Channel = Channel + 1,
                        Overpass = Overpass,
                        Way = Way,
                        Product = Product,
                        Riser = Riser
                    };
            }
        }

        public RiserWaggonData WaggonData
        {
            get
            {
                return new RiserWaggonData
                    {
                        Number = Number,
                        Ntype = Ntype,
                        FactHeight = FactHeight,
                        Setpoint = Setpoint,
                        FilledLevel = FilledLevel
                    };
            }
        }

        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active == value) return;
                _active = value;
                if (_active)
                    Actived = false;
            }
        }

        public bool Actived { get; set; }

        public bool Linked { get; set; }
        public DateTime NextFetching { get; set; }
        public long TotalRequests { get; set; }

        public long TotalErrors { get; set; }

        public int BarometerValue { get; set; }

        public void UpdateRiserPropertyToRemote()
        {
            var riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                      Channel, Overpass, Way, Product, Riser);
            Data.UpdateRiserProperty(riserName, "TotalRequests", TotalRequests.ToString("0"));
            Data.UpdateRiserProperty(riserName, "TotalErrors", TotalErrors.ToString("0"));
            Data.UpdateRiserProperty(riserName, "BarometerValue", BarometerValue.ToString("0"));
            Data.UpdateRiserProperty(riserName, "Channel", Channel.ToString("0"));
        }

        public int MarginalLimit { get; set; }
        public int FailLimit { get; set; }
        public FetchingStatus Status { get; set; }
        public TimeSpan TimeMarginal { get; set; }
        public TimeSpan TimeFail { get; set; }

        public ushort[] Hregs
        {
            get { return _hregs; }
            set { _hregs = value; }
        }

        public ushort[] Oldregs
        {
            get { return _oldregs; }
        }

        public int WriteAddress { get; set; }
        public ushort[] WriteData { get; set; }

        public void UpdateData(ushort[] hregs, int start, int count, bool remoted = false)
        {
            if (start == 0 && count == 0)
            {
                _hregs = new ushort[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                    };
                _oldregs = new ushort[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                    };
            }
            else if ((start + count) <= 61)
            {
                Array.Copy(_hregs, _oldregs, 61);
                Array.Copy(hregs, 0, _hregs, start, count);
            }
            else
                return;
            Ready = false;
            HasAlarmLevel = false;
            HasNoGround = null;
            HasHandMode = false;
            WorkMode = false;
            SmallValve = false;
            BigValve = false;
            StopCause = 0;
            if (_hregs[0] != 0xFFFF && _hregs[1] != 0xFFFF && _hregs[2] != 0xFFFF && _hregs[5] != 0xFFFF)
            {
                var level = _hregs[0];
                var calcLevel = level > 32767 ? level - 65536 : level;
                CurrentLevel = calcLevel;
                if (Data.UseSmartLevel)
                {
                    //_queue.Add(_lastLevel - calcLevel);
                    //if (_queue.Count > Data.SmartLevelQueueSize) _queue.RemoveAt(0);
                    //FreshedValue = Math.Abs(_queue.Average()) > Data.SmartLevelDifferent;
                    FreshedValue = _hregs[0x24] >= 5000;
                    if (FilledKind == null) // идет налив
                    {
                        ShowedLevel = FreshedValue ? calcLevel : 0;
                        _lastLevel = calcLevel;
                    }
                    else
                        ShowedLevel = calcLevel;
                }
                else
                    ShowedLevel = calcLevel;
                HasNoGround = (_hregs[1] & 0x0400) > 0;
                HasHandMode = (_hregs[1] & 0x0800) > 0;
                StopCause = _hregs[2];
                SetStopCount(_hregs[1] & 0xFF, remoted);
                if (!remoted)
                {
                    var riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                          Channel, Overpass, Way, Product, Riser);
                    var filledKind = (StopCause & 0x2000) > 0 ? "1" : "0";
                    Data.UpdateRiserProperty(riserName, "FilledKind", filledKind);
                    Data.UpdateRiserProperty(riserName, "FilledLevel", FilledLevel.ToString("0"));
                    Data.UpdateRiserProperty(riserName, "HasNoGround", (bool)HasNoGround ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "HasHandMode", HasHandMode ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "StopCause", StopCause.ToString("0"));
                    Data.UpdateRiserProperty(riserName, "FreshedValue", FreshedValue ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "ShowedLevel", ShowedLevel.ToString("0"));
                }
            }
            if (_hregs[1] != 0xFFFF && _hregs[3] != 0xFFFF && _hregs[4] != 0xFFFF)
            {
                Ready = (_hregs[3] & 0x08) > 0;
                BigValve = (_hregs[1] & 0x1000) > 0 || (_hregs[3] & 0x02) > 0;
                SmallValve = (_hregs[1] & 0x2000) > 0 || (_hregs[3] & 0x04) > 0;
                WorkMode = (_hregs[1] & 0x4000) > 0 || (_hregs[3] & 0x10) > 0;
                HasAlarmLevel = (_hregs[1] & 0x0100) > 0 ||
                    (_hregs[3] & 0xC000) > 0 || (_hregs[4] & 0x0001) > 0;
                if (!remoted)
                {
                    var riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                          Channel, Overpass, Way, Product, Riser);
                    Data.UpdateRiserProperty(riserName, "Ready", Ready ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "BigValve", BigValve ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "SmallValve", SmallValve ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "WorkMode", WorkMode ? "1" : "0");
                    Data.UpdateRiserProperty(riserName, "HasAlarmLevel", HasAlarmLevel ? "1" : "0");
                }
            }
            if (_hregs[5] != 0xFFFF)
            {
                var state = _hregs[5];
                switch (state)
                {
                    case 0:
                        RiserMode = RiserState.None;
                        break;
                    case 1:
                        RiserMode = RiserState.Waiting;
                        break;
                    case 2:
                        RiserMode = RiserState.HandWaiting;
                        break;
                    case 3:
                        RiserMode = RiserState.HandSmallFilling;
                        break;
                    case 4:
                        RiserMode = RiserState.HandBigFilling;
                        break;
                    case 5:
                        RiserMode = RiserState.SmallFilling;
                        break;
                    case 6:
                        RiserMode = RiserState.BigFilling;
                        break;
                    case 7:
                        RiserMode = RiserState.FillingByAuto;
                        break;
                    case 8:
                        RiserMode = RiserState.FillingByOper;
                        break;
                    case 9:
                        RiserMode = RiserState.Filled;
                        break;
                }
                if (!remoted)
                {
                    var riserName = string.Format("{0}_{1}_{2}_{3}_{4}",
                                                  Channel, Overpass, Way, Product, Riser);
                    Data.UpdateRiserProperty(riserName, "RiserMode", ((int)RiserMode).ToString("0"));
                }
            }
            if (_hregs[0x35] != 0xFFFF) DeepLevel = _hregs[0x35];
            if (_hregs[0x36] != 0xFFFF) WorkLevel = _hregs[0x36];

            // заполнение журнала переключений
            if (_oldregs.Length != _hregs.Length || remoted) return;
            for (var i = 0; i < _hregs.Length; i++)
            {
                if (i == 1 && (_oldregs[i] & 0xFF00) == (_hregs[i] & 0xFF00))
                    continue;
                if (_oldregs[i] == _hregs[i] && !((DateTime.Now - _lastFething).TotalSeconds > 60)) continue;
                var oldflags = Convert.ToString(_oldregs[i], 2);
                if (oldflags.Length < 16)
                    oldflags = string.Concat(new string('0', 16 - oldflags.Length), oldflags);
                oldflags = Reverse(oldflags);
                var newflags = Convert.ToString(_hregs[i], 2);
                if (newflags.Length < 16)
                    newflags = string.Concat(new string('0', 16 - newflags.Length), newflags);
                newflags = Reverse(newflags);
                var regname = Convert.ToString(i, 16);
                if (regname.Length < 2)
                    regname = string.Concat(new string('0', 2 - regname.Length), regname);
                regname = string.Concat("HR", regname);

                List<int> exceptbits;
                switch (i)
                {
                    case 0x01:
                        for (var j = 8; j < 16; j++)
                        {
                            if (oldflags[j] == newflags[j]) continue;
                            Data.SendToSwitchLog(Name, Address, string.Concat(regname, ".", j),
                                                 new string(oldflags[j], 1),
                                                 new string(newflags[j], 1), GetFlagDescriptor(i, j));
                        }
                        break;
                    case 0x02:
                        for (var j = 0; j < 16; j++)
                        {
                            if (oldflags[j] == newflags[j]) continue;
                            Data.SendToSwitchLog(Name, Address, string.Concat(regname, ".", j),
                                                 new string(oldflags[j], 1),
                                                 new string(newflags[j], 1), GetFlagDescriptor(i, j));
                        }
                        break;
                    case 0x03:
                        exceptbits = new List<int> {8, 9, 12, 13};
                        for (var j = 0; j < 16; j++)
                        {
                            if (exceptbits.IndexOf(j) >= 0) continue;
                            if (oldflags[j] == newflags[j]) continue;
                            Data.SendToSwitchLog(Name, Address, string.Concat(regname, ".", j),
                                                 new string(oldflags[j], 1),
                                                 new string(newflags[j], 1), GetFlagDescriptor(i, j));
                        }
                        break;
                    case 0x04:
                        exceptbits = new List<int> {10, 11};
                        for (var j = 0; j < 16; j++)
                        {
                            if (exceptbits.IndexOf(j) >= 0) continue;
                            if (oldflags[j] == newflags[j]) continue;
                            Data.SendToSwitchLog(Name, Address, string.Concat(regname, ".", j),
                                                 new string(oldflags[j], 1),
                                                 new string(newflags[j], 1), GetFlagDescriptor(i, j));
                        }
                        break;
                }
                var level = _hregs[0x00];
                var maxheight = _hregs[0x07];
                var levlow = _hregs[0x07] - _hregs[0x35];
                var levhigh = levlow + _hregs[0x36];
                var levbigoff = _hregs[0x08] - _hregs[0x1A];
                var setpoint = _hregs[0x08];
                var analoglevelcurrent = _hregs[0x24];
                var alarmlevelcurrent = _hregs[0x25];
                // ReSharper disable InconsistentNaming
                var hr3_3 = (_hregs[0x03] & 0x0008) > 0;
                var hr3_4 = (_hregs[0x03] & 0x0010) > 0;
                var hr1_14 = (_hregs[0x01] & 0x4000) > 0;
                var hr4_9 = (_hregs[0x04] & 0x0200) > 0;
                var hr3_2 = (_hregs[0x03] & 0x0004) > 0;
                var hr1_13 = (_hregs[0x01] & 0x2000) > 0;
                var hr4_13 = (_hregs[0x04] & 0x2000) > 0;
                var hr4_8 = (_hregs[0x04] & 0x0100) > 0;
                var hr3_1 = (_hregs[0x03] & 0x0002) > 0;
                var hr1_12 = (_hregs[0x01] & 0x1000) > 0;
                var hr4_12 = (_hregs[0x04] & 0x1000) > 0;
                var hr1_8 = (_hregs[0x01] & 0x0100) > 0;
                var hr3_14 = (_hregs[0x03] & 0x4000) > 0;
                var hr3_15 = (_hregs[0x03] & 0x8000) > 0;
                var hr3_6 = (_hregs[0x03] & 0x0040) > 0;
                var hr3_10 = (_hregs[0x03] & 0x0400) > 0;
                var hr3_11 = (_hregs[0x03] & 0x0800) > 0;
                // ReSharper restore InconsistentNaming
                Data.SendToTrend(string.Concat(Name, "_"), Address,
                                 level, 0, maxheight, levlow, levhigh, levbigoff, setpoint,
                                 analoglevelcurrent, alarmlevelcurrent,
                                 hr3_3, hr3_4,hr1_14,hr4_9,hr3_2,hr1_13,hr4_13,hr4_8,
                                 hr3_1,hr1_12,hr4_12,hr1_8,hr3_14,hr3_15,hr3_6,hr3_10,hr3_11);
                _lastFething = DateTime.Now;
            }
        }

        private static string Reverse(string values)
        {
            var sb = new StringBuilder();
            foreach (var value in values)
                sb.Insert(0, value);
            return sb.ToString();
        }

        public bool Ready { get; set; }
        public bool HasAlarmLevel { get; set; }
        public bool? HasNoGround { get; set; }
        public bool HasHandMode { get; set; }
        public bool WorkMode { get; set; }
        public bool SmallValve { get; set; }

        public bool BigValve { get; set; }
        public bool Selected { get; set; }
        public int Setpoint { get; set; }
        public string Ntype { get; set; }
        public int CurrentLevel { get; set; }
        public int ShowedLevel { get; set; }
        public int FilledLevel { get; set; }
        public bool FreshedValue { get; set; }

        public bool? FilledKind { get; set; }
        public bool StartPressed { get; set; }
        public bool StopPressed { get; set; }

        public void SetStopCount(int value, bool remoted)
        {
            var stoped = value > _stopCount && RiserMode > RiserState.Waiting;
            _stopCount = value;
            if (!stoped) return;
            FillingEnded = DateTime.Now;
            FilledKind = (StopCause & 0x2000) > 0;
            FilledLevel = CurrentLevel;
            //------------------------------------------------
            var masks = new[]
                {
                    0x01,
                    0x02,
                    0x04,
                    0x08,
                    0x10,
                    0x20,
                    0x40,
                    0x80,
                    0x0100,
                    0x0200,
                    0x0400,
                    0x0800,
                    0x1000,
                    0x2000,
                    0x4000,
                    0x8000
                };
            var messages = new[]
                {
                    "Налив завершен аварийно. Сработал сигнализатор аварийный",
                    "Налив завершен кнопкой \"СТОП\" пульта управления",
                    "Налив завершен аварийно. Неисправность цепи готовности",
                    "Налив завершен аварийно. Неисправность сигнализатора уровня",
                    "Налив завершен аварийно. Истекло время работы без связи",
                    "Налив завершен аварийно. Заземление отсутствует",
                    "Налив завершен аварийно. Ошибка клапана большого прохода",
                    "Налив завершен аварийно. Ошибка клапана малого прохода",
                    "Налив завершен аварийно. Ток сигнализатора уровня меньше минимального",
                    "Налив завершен аварийно. Ток сигнализатора уровня больше максимального",
                    "Налив завершен аварийно. Ток сигнализатора аварийного меньше минимального",
                    "Налив завершен аварийно. Ток сигнализатора аварийного больше максимального",
                    "Налив завершен аварийно. Нет рабочего положения",
                    "Налив завершен автоматически",
                    "Налив завершен оператором АРМ",
                    "Налив завершен аварийно. Неверные данные налива"
                };
            string stopcause = null;
            for (var i = 0; i < masks.Length; i++)
            {
                if ((StopCause & masks[i]) <= 0) continue;
                if (!remoted)
                    Data.SendToSystemLog(messages[i], Address, WaggonData);
                if (stopcause == null)
                    stopcause = messages[i];
            }
            if (stopcause != null && !remoted)
                Data.SendToFillingLog(FillingStarted, Address, WaggonData, FillingEnded, FillingUser, stopcause);
        }

        public int StopCause { get; set; }
        public RiserState RiserMode { get; set; }
        public int? DeepLevel { get; set; }
        public int? WorkLevel { get; set; }

        public bool LevelCheckMode
        {
            get
            {
                return Number.Trim().Length > 0 && Ntype.Trim().Length > 0
                       && FactHeight > 0 && Setpoint > 0;
            }
        }

        public string Number { get; set; }
        public int FactHeight { get; set; }

        public DateTime FillingStarted { get; set; }
        public DateTime FillingEnded { get; set; }
        public string FillingUser { get; set; }

        private static string GetFlagDescriptor(int reg, int bit)
        {
            switch (reg)
            {
                case 1:
                    switch (bit)
                    {
                        case 8:
                            return "Состояние сигнализатора аварийного (1 - мокрый)";
                        case 10:
                            return "Состояние заземления (1 - отсутствует)";
                        case 11:
                            return "Автономный режим";
                        case 12:
                            return "Состояние клапана большого прохода";
                        case 13:
                            return "Состояние клапана малого прохода";
                        case 14:
                            return "Состояние конечника рабочего положения";
                        case 15:
                            return "Прочие неисправности";
                    }
                    break;
                case 2:
                    switch (bit)
                    {
                        case 0:
                            return "Налив завершен аварийно. Сработал сигнализатор аварийный";
                        case 1:
                            return "Налив завершен кнопкой \"СТОП\" пульта управления";
                        case 2:
                            return "Налив завершен аварийно. Неисправность цепи готовности";
                        case 3:
                            return "Налив завершен аварийно. Неисправность сигнализатора уровня";
                        case 4:
                            return "Налив завершен аварийно. Истекло время работы без связи";
                        case 5:
                            return "Налив завершен аварийно. Заземление отсутствует";
                        case 6:
                            return "Налив завершен аварийно. Ошибка клапана большого прохода";
                        case 7:
                            return "Налив завершен аварийно. Ошибка клапана малого прохода";
                        case 8:
                            return "Налив завершен аварийно. Ток сигнализатора уровня меньше минимального";
                        case 9:
                            return "Налив завершен аварийно. Ток сигнализатора уровня больше максимального";
                        case 10:
                            return "Налив завершен аварийно. Ток сигнализатора аварийного меньше минимального";
                        case 11:
                            return "Налив завершен аварийно. Ток сигнализатора аварийного больше максимального";
                        case 12:
                            return "Налив завершен аварийно. Нет рабочего положения";
                        case 13:
                            return "Налив завершен автоматически";
                        case 14:
                            return "Налив завершен оператором АРМ";
                        case 15:
                            return "Налив завершен аварийно. Неверные данные налива";
                    }
                    break;
                case 3:
                    switch (bit)
                    {
                        case 0:
                            return "Кнопка \"ПУСК\"";
                        case 1:
                            return "Конечник клапана большого прохода";
                        case 2:
                            return "Конечник клапана малого прохода";
                        case 3:
                            return "Цепь готовности";
                        case 4:
                            return "Конечник рабочего положения";
                        case 5:
                            return "Контроль заземления";
                        case 6:
                            return "Контроль сигнализатора уровня";
                        case 7:
                            return "Кнопка \"АВТОНОМНО\"";
                        case 10:
                            return "Ток сигнализатора уровня меньше минимального";
                        case 11:
                            return "Ток сигнализатора уровня больше максимального";
                        case 14:
                            return "Ток сигнализатора аварийного меньше минимального";
                        case 15:
                            return "Ток сигнализатора аварийного больше максимального";
                    }
                    break;
                case 4:
                    switch (bit)
                    {
                        case 0:
                            return "Состояние сигнализатора аварийного (1 - мокрый)";
                        case 1:
                            return "Кнопка \"СТОП\"";
                        case 2:
                            return "Цепь готовности неисправна";
                        case 3:
                            return "Сигнализатор уровня неисправен";
                        case 4:
                            return "Истекло время работы без связи";
                        case 5:
                            return "Заземление отсутствует";
                        case 6:
                            return "Ошибка клапана большого прохода";
                        case 7:
                            return "Ошибка клапана малого прохода";
                        case 8:
                            return "Команда включения клапана большого прохода";
                        case 9:
                            return "Команда включения клапана малого прохода";
                        case 12:
                            return "Состояние клапана большого прохода";
                        case 13:
                            return "Состояние клапана малого прохода";
                        case 14:
                            return "Состояние зеленого индикатора панели управления";
                        case 15:
                            return "Состояние синего индикатора панели управления";
                    }
                    break;
            }
            return "";
        }

        public void RemoteClientUpdatedRiserNodeRegister(ushort reg, ushort val)
        {
            UpdateData(new[] {val}, reg, 1, true);
        }

        public void RemoteClientUpdatedRiserNodeValue(string regname, string regvalue)
        {
            var riserAddr = string.Format("N{0}{1}{2}{3}", Overpass, Way, Product, Riser.ToString("000"));
            int value;
            long lval;
            switch (regname)
            {
                case "TotalRequests":
                    if (long.TryParse(regvalue, out lval))
                        TotalRequests = lval;
                    break;
                case "TotalErrors":
                    if (long.TryParse(regvalue, out lval))
                        TotalErrors = lval;
                    break;
                case "BarometerValue":
                    if (int.TryParse(regvalue, out value))
                        BarometerValue = value;
                    break;
                case "RiserMode":
                    if (int.TryParse(regvalue, out value) && value >= 0 && value <= 9)
                        RiserMode = (RiserState)value;
                    break;
                case "Ready":
                    Ready = regvalue == "1";
                    break;
                case "BigValve":
                    BigValve = regvalue == "1";
                    break;
                case "SmallValve":
                    SmallValve = regvalue == "1";
                    break;
                case "WorkMode":
                    WorkMode = regvalue == "1";
                    break;
                case "HasAlarmLevel":
                    HasAlarmLevel = regvalue == "1";
                    break;
                case "HasNoGround":
                    HasNoGround = regvalue == "1";
                    break;
                case "HasHandMode":
                    HasHandMode = regvalue == "1";
                    break;
                case "FreshedValue":
                    FreshedValue = regvalue == "1";
                    break;
                case "FilledKind":
                    if (string.IsNullOrWhiteSpace(regvalue))
                        FilledKind = null;
                    else if (regvalue == "1")
                        FilledKind = true;
                    else
                        FilledKind = false;
                    break;
                case "ShowedLevel":
                    if (int.TryParse(regvalue, out value))
                        ShowedLevel = value;
                    break;
                case "FilledLevel":
                    if (int.TryParse(regvalue, out value))
                        FilledLevel = value;
                    break;
                case "Number":
                    Number = regvalue;
                    //Data.Tasks.WriteInteger(riserAddr, "Overpass", Overpass);
                    //Data.Tasks.WriteInteger(riserAddr, "Way", Way);
                    //Data.Tasks.WriteString(riserAddr, "Product", Product);
                    //Data.Tasks.WriteInteger(riserAddr, "Riser", Riser);
                    //Data.Tasks.WriteString(riserAddr, "Number", Number);
                    //Data.Tasks.UpdateFile();
                    break;
                case "Ntype":
                    Ntype = regvalue;
                    //Data.Tasks.WriteInteger(riserAddr, "Overpass", Overpass);
                    //Data.Tasks.WriteInteger(riserAddr, "Way", Way);
                    //Data.Tasks.WriteString(riserAddr, "Product", Product);
                    //Data.Tasks.WriteInteger(riserAddr, "Riser", Riser);
                    //Data.Tasks.WriteString(riserAddr, "NType", Ntype);
                    //Data.Tasks.UpdateFile();
                    break;
                case "Setpoint":
                    if (int.TryParse(regvalue, out value))
                    {
                        Setpoint = value;
                        //Data.Tasks.WriteInteger(riserAddr, "Overpass", Overpass);
                        //Data.Tasks.WriteInteger(riserAddr, "Way", Way);
                        //Data.Tasks.WriteString(riserAddr, "Product", Product);
                        //Data.Tasks.WriteInteger(riserAddr, "Riser", Riser);
                        //Data.Tasks.WriteInteger(riserAddr, "Setpoint", Setpoint);
                        //Data.Tasks.UpdateFile();
                    }
                    break;
                case "FactHeight":
                    if (int.TryParse(regvalue, out value))
                    {
                        FactHeight = value;
                        //Data.Tasks.WriteInteger(riserAddr, "Overpass", Overpass);
                        //Data.Tasks.WriteInteger(riserAddr, "Way", Way);
                        //Data.Tasks.WriteString(riserAddr, "Product", Product);
                        //Data.Tasks.WriteInteger(riserAddr, "Riser", Riser);
                        //Data.Tasks.WriteInteger(riserAddr, "FactHeight", FactHeight);
                        //Data.Tasks.UpdateFile();
                    }
                    break;
            }
        }
    }
}