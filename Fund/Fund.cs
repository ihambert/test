using System.Collections.Generic;

namespace Fund
{
    public class Fund
    {
        /// <summary>
        ///     基金代码
        /// </summary>
        public string Code;

        /// <summary>
        ///     基金名称
        /// </summary>
        public string Name;

        /// <summary>
        ///     估算涨幅
        /// </summary>
        public double Rate;

        /// <summary>
        ///     2日涨幅
        /// </summary>
        public double Rate2;

        /// <summary>
        ///     3日涨幅
        /// </summary>
        public double Rate3;

        /// <summary>
        ///     5日涨幅
        /// </summary>
        public double Rate5;

        /// <summary>
        ///     1周涨幅
        /// </summary>
        public double Rate7;

        /// <summary>
        ///     1月涨幅
        /// </summary>
        public double Rate30;

        /// <summary>
        ///     2月涨幅
        /// </summary>
        public double Rate60;

        /// <summary>
        ///     3月涨幅
        /// </summary>
        public double Rate90;

        /// <summary>
        ///     连续多天涨幅
        /// </summary>
        public List<double> Rates;

        /// <summary>
        /// 基金类型，1：A股 2：债券 3：其他
        /// </summary>
        public int Type;

        /// <summary>
        /// 手续费
        /// </summary>
        public float Fee;

        /// <summary>
        /// 7天后手续费
        /// </summary>
        public float Fee7;
    }
}