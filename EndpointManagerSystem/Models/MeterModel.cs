namespace EndpointManager.Models
{
    public static class MeterModel
    {
        private enum MeterModelId
        {
            NSX1P2W = 16,
            NSX1P3W = 17,
            NSX2P3W = 18,
            NSX3P4W = 19
        }

        public static string Name(int meterModelId)
        {
            return Enum.GetName(typeof(MeterModelId), meterModelId) ?? throw new Exception("Meter Model not found.");
        }

        public static int Value(string meterModel)
        {
            if (Enum.TryParse<MeterModelId>(meterModel, true, out MeterModelId meterModelId))
                return (int) meterModelId;
            else
                throw new Exception("Meter Model not found.");
        }

        public static string[] NameOptions()
        {
            return Enum.GetNames(typeof(MeterModelId));
        }

        public static int[] ValueOptions()
        {
            return (int[]) Enum.GetValues(typeof(MeterModelId)).Cast<int>();
        }
    }
}