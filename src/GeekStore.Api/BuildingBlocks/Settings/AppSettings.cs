namespace GeekStore.Api.BuildingBlocks.Settings
{
    public class AppSettings
    {
        #region Properties

        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }

        #endregion
    }
}