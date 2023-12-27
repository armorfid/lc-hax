#pragma warning disable IDE1006

using HarmonyLib;

namespace Hax;

[HarmonyPatch(typeof(GrabbableObject))]
[HarmonyPatch("GrabClientRpc")]
class ChargeAnyItemPatch {
    static void Postfix(ref Item ___itemProperties) {
        ___itemProperties.requiresBattery = true;
    }
}
