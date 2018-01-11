using System;
using System.Collections.Generic;

namespace GradingFund
{
    public class GradingFundData
    {
        /// <summary>
        ///     概念资金流向
        /// </summary>
        public readonly List<Industry> ConceptFlow;

        /// <summary>
        ///     分级基金
        /// </summary>
        public List<GradingFund> GradingFunds;

        /// <summary>
        ///     行业资金流向
        /// </summary>
        public readonly List<Industry> IndustryFlow;

        /// <summary>
        ///     分级重仓的个股
        /// </summary>
        public readonly List<Stock> Stocks;

        /// <summary>
        ///     近1分钟大盘流入资金（单位：亿元）
        /// </summary>
        public double Inf;

        /// <summary>
        ///     大盘主力总流入资金（单位：亿元）
        /// </summary>
        public double Inflow;

        /// <summary>
        ///     是否是可交易时间
        /// </summary>
        public bool IsDealTime
        {
            get
            {
                var now = DateTime.Now;
                return now > _jjTime && now < _zspTime || now > _zkpTime && now < _spTime;
            }
        }

        /// <summary>
        /// 是否已开盘
        /// </summary>
        public bool IsOpen => DateTime.Now > _kpTime;

        /// <summary>
        /// 判断数据是否已经初始化
        /// </summary>
        public bool IsInit;

        public GradingFundData()
        {
            Stocks = new List<Stock>();
            GradingFunds = new List<GradingFund>();
            IndustryFlow = new List<Industry>();
            ConceptFlow = new List<Industry>();
            var now = DateTime.Now;
            _jjTime = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            _kpTime = new DateTime(now.Year, now.Month, now.Day, 9, 30, 0);
            _zspTime = new DateTime(now.Year, now.Month, now.Day, 11, 30, 0);
            _zkpTime = new DateTime(now.Year, now.Month, now.Day, 13, 0, 0);
            //近收盘时不能交易了
            _spTime = new DateTime(now.Year, now.Month, now.Day, 14, 58, 0);
        }

        /// <summary>
        ///     竞价时间
        /// </summary>
        private readonly DateTime _jjTime;

        /// <summary>
        ///     开盘时间
        /// </summary>
        private readonly DateTime _kpTime;

        /// <summary>
        ///     上午收盘时间
        /// </summary>
        private readonly DateTime _zspTime;

        /// <summary>
        ///     下午开盘时间
        /// </summary>
        private readonly DateTime _zkpTime;

        /// <summary>
        ///     收盘时间
        /// </summary>
        private readonly DateTime _spTime;
    }
}