using System;
using System.Collections.Generic;

namespace Fund
{
    public class FundData
    {
        /// <summary>
        ///     基金
        /// </summary>
        public List<Fund> Funds;

        /// <summary>
        ///     是否是可交易时间
        /// </summary>
        public bool IsDealTime
        {
            get
            {
                var now = DateTime.Now;
                return now > _kpTime && now < _zspTime || now > _zkpTime && now < _spTime;
            }
        }

        public FundData()
        {
            Funds = new List<Fund>();
            var now = DateTime.Now;
            _kpTime = new DateTime(now.Year, now.Month, now.Day, 9, 31, 0);
            _zspTime = new DateTime(now.Year, now.Month, now.Day, 11, 30, 0);
            _zkpTime = new DateTime(now.Year, now.Month, now.Day, 13, 1, 0);
            _spTime = new DateTime(now.Year, now.Month, now.Day, 14, 32, 0);
        }

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