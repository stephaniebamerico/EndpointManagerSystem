namespace EndpointManager.Models
{
    public static class SwitchState
    {
        // All Switch States accepted
        private enum SwitchStateId
        {
            Disconnected = 0,
            Connected = 1,
            Armed = 2
        }

        public static string Name(int switchState)
        {
            return Enum.GetName(typeof(SwitchStateId), switchState) ?? throw new Exception("Switch State not valid.");
        }

        public static int Value(string switchState)
        {
            if (Enum.TryParse<SwitchStateId>(switchState, true, out SwitchStateId switchStateId))
                return (int) switchStateId;
            else
                throw new Exception("Switch State not valid.");
        }

        public static string[] NameOptions()
        {
            return Enum.GetNames(typeof(SwitchStateId));
        }

        public static int[] ValueOptions()
        {
            return (int[]) Enum.GetValues(typeof(SwitchStateId)).Cast<int>();
        }
    }
}