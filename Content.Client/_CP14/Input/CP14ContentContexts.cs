using Content.Shared._CE14.Input;
using Robust.Shared.Input;

namespace Content.Client._CE14.Input
{
    public static class CE14ContentContexts
    {
        public static void SetupContexts(IInputContextContainer contexts)
        {
            var human = contexts.GetContext("human");
            human.AddFunction(CE14ContentKeyFunctions.OpenBelt2);
            human.AddFunction(CE14ContentKeyFunctions.SmartEquipBelt2);
            human.AddFunction(CE14ContentKeyFunctions.CE14OpenSkillMenu);
        }
    }
}
