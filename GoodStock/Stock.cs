namespace GoodStock
{
    public class Stock
    {
        /// <summary>
        ///     市盈率
        /// </summary>
        public float Ape;

        /// <summary>
        ///     量比
        /// </summary>
        public float Cat;

        /// <summary>
        ///     推荐个数
        /// </summary>
        public int ChooseCount;

        /// <summary>
        ///     股票代号
        /// </summary>
        public string Code;

        /// <summary>
        ///     抄底指数（越大越到当天最低价）
        /// </summary>
        public double Hunter;

        /// <summary>
        ///     股票所属行业
        /// </summary>
        public string Industry;

        /// <summary>
        ///     最高价
        /// </summary>
        public float MaxPrice;

        /// <summary>
        ///     最低价
        /// </summary>
        public float MinPrice;

        /// <summary>
        ///     股票名称
        /// </summary>
        public string Name;

        /// <summary>
        ///     当前价
        /// </summary>
        public float Price;

        /// <summary>
        ///     涨跌幅
        /// </summary>
        public double Rate;

        /// <summary>
        ///     今开
        /// </summary>
        public float StartPrice;

        /// <summary>
        ///     振幅（%）
        /// </summary>
        public double Swing;

        /// <summary>
        ///     成交额（亿）
        /// </summary>
        public float Tm;

        /// <summary>
        ///     换手率
        /// </summary>
        public float Tr;

        /// <summary>
        ///     涨停价
        /// </summary>
        public float UpPrice;

        /// <summary>
        ///     昨收
        /// </summary>
        public float YesterdayPrice;
    }
}