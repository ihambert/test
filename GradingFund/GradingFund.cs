using System.Collections.Generic;

namespace GradingFund
{
    public class GradingFund
    {
        /// <summary>
        /// 开盘价
        /// </summary>
        public float StartPrice;

        /// <summary>
        /// 昨日收盘价
        /// </summary>
        public float YesterdayPrice;

        /// <summary>
        /// 当前价
        /// </summary>
        public float Price;

        /// <summary>
        /// 最高价
        /// </summary>
        public float MaxPrice;

        /// <summary>
        /// 最低价
        /// </summary>
        public float MinPrice;

        /// <summary>
        ///     平均涨幅
        /// </summary>
        public double AvgRate;

        /// <summary>
        ///     分级代码
        /// </summary>
        public string Code;

        /// <summary>
        ///     估算涨幅
        /// </summary>
        public float EstimateRate;

        /// <summary>
        ///     龙头股票
        /// </summary>
        public Stock Leader;

        /// <summary>
        ///     一分钟快速上涨股票
        /// </summary>
        public Stock MaxSpeed;

        /// <summary>
        ///     分级名称
        /// </summary>
        public string Name;

        /// <summary>
        ///     涨幅
        /// </summary>
        public double Rate;

        /// <summary>
        /// 振幅
        /// </summary>
        public double Swing;

        /// <summary>
        ///     前十重仓股红的比例
        /// </summary>
        public int RedRate;

        /// <summary>
        ///     涨速（重仓股的平均涨速）
        /// </summary>
        public double Speed;

        /// <summary>
        ///     分级重仓股票
        /// </summary>
        public List<Stock> Stocks;

        /// <summary>
        ///     分级重仓股票持有比例
        /// </summary>
        public List<float> HoldingRatio;
    }
}