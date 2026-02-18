using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace BitchlandMoreWeaponSlotsBepInEx
{
    [BepInPlugin("com.wolfitdm.BitchlandMoreWeaponSlotsBepInEx", "BitchlandMoreWeaponSlotsBepInEx Plugin", "1.0.0.0")]
    public class BitchlandMoreWeaponSlotsBepInEx : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private static ConfigEntry<bool> configEnableMe;

        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon1;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon2;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon3;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon4;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon5;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon6;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon7;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon8;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon9;
        private static ConfigEntry<KeyCode> configMoreWeaponSlotsSwitchWeapon10;

        public BitchlandMoreWeaponSlotsBepInEx()
        {
        }

        public static Type MyGetType(string originalClassName)
        {
            return Type.GetType(originalClassName + ",Assembly-CSharp");
        }

        public static Type MyGetTypeUnityEngine(string originalClassName)
        {
            return Type.GetType(originalClassName + ",UnityEngine");
        }

        private static string pluginKey = "General.Toggles";
        private static string pluginKeyControlsMoreWeaponSlots = "NoreWeaponSlots.KeyControls";

        public static bool enableMe = false;
        public static KeyCode KeyCodeSwitchWeapon1 = 0;
        public static KeyCode KeyCodeSwitchWeapon2 = 0;
        public static KeyCode KeyCodeSwitchWeapon3 = 0;
        public static KeyCode KeyCodeSwitchWeapon4 = 0;
        public static KeyCode KeyCodeSwitchWeapon5 = 0;
        public static KeyCode KeyCodeSwitchWeapon6 = 0;
        public static KeyCode KeyCodeSwitchWeapon7 = 0;
        public static KeyCode KeyCodeSwitchWeapon8 = 0;
        public static KeyCode KeyCodeSwitchWeapon9 = 0;
        public static KeyCode KeyCodeSwitchWeapon10 = 0;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableMe",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");

            configMoreWeaponSlotsSwitchWeapon1 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon1",
                     KeyCode.Alpha1,
                     "KeyCode to switch weapon 1, default Number 1");

            configMoreWeaponSlotsSwitchWeapon2 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon2",
                      KeyCode.Alpha2,
                     "KeyCode to switch weapon 2, default Number 2");

            configMoreWeaponSlotsSwitchWeapon3 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                                 "KeyCodeSwitchWeapon3",
                                  KeyCode.Alpha3,
                                 "KeyCode to switch weapon 3, default Number 3");

            configMoreWeaponSlotsSwitchWeapon4 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon4",
                      KeyCode.Alpha4,
                     "KeyCode to switch weapon 4, default Number 4");

            configMoreWeaponSlotsSwitchWeapon5 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon5",
                      KeyCode.Alpha5,
                     "KeyCode to switch weapon 5, default Number 5");

            configMoreWeaponSlotsSwitchWeapon6 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon6",
                      KeyCode.Alpha6,
                     "KeyCode to switch weapon 6, default Number 6");

            configMoreWeaponSlotsSwitchWeapon7 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon7",
                      KeyCode.Alpha7,
                     "KeyCode to switch weapon 7, default Number 7");

            configMoreWeaponSlotsSwitchWeapon8 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon8",
                      KeyCode.Alpha8,
                     "KeyCode to switch weapon 8, default Number 8");

            configMoreWeaponSlotsSwitchWeapon9 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                     "KeyCodeSwitchWeapon9",
                      KeyCode.Alpha9,
                     "KeyCode to switch weapon 9, default Number 9");

            configMoreWeaponSlotsSwitchWeapon10 = Config.Bind(pluginKeyControlsMoreWeaponSlots,
                    "KeyCodeSwitchWeapon10",
                    KeyCode.Alpha0,
                    "KeyCode to switch weapon 10, default Number 0");

            enableMe = configEnableMe.Value;
            KeyCodeSwitchWeapon1 = configMoreWeaponSlotsSwitchWeapon1.Value;
            KeyCodeSwitchWeapon2 = configMoreWeaponSlotsSwitchWeapon2.Value;
            KeyCodeSwitchWeapon3 = configMoreWeaponSlotsSwitchWeapon3.Value;
            KeyCodeSwitchWeapon4 = configMoreWeaponSlotsSwitchWeapon4.Value;
            KeyCodeSwitchWeapon5 = configMoreWeaponSlotsSwitchWeapon5.Value;
            KeyCodeSwitchWeapon6 = configMoreWeaponSlotsSwitchWeapon6.Value;
            KeyCodeSwitchWeapon7 = configMoreWeaponSlotsSwitchWeapon7.Value;
            KeyCodeSwitchWeapon8 = configMoreWeaponSlotsSwitchWeapon8.Value;
            KeyCodeSwitchWeapon9 = configMoreWeaponSlotsSwitchWeapon9.Value;
            KeyCodeSwitchWeapon10 = configMoreWeaponSlotsSwitchWeapon10.Value;

            PatchAllHarmonyMethods();

            Logger.LogInfo($"Plugin BitchlandMoreWeaponSlotsBepInEx BepInEx is loaded!");
        }
		
		public static void PatchAllHarmonyMethods()
        {
			if (!enableMe)
            {
                return;
            }
			
            try
            {
                PatchHarmonyMethodUnity(typeof(WeaponSystem), "Update", "WeaponSystem_Update", true, false);
                PatchHarmonyMethodUnity(typeof(WeaponSystem), "SetActiveWeapon", "WeaponSystem_SetActiveWeapon", true, false, new Type[] { typeof(int) });
            } catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
        }
		
		public static void PatchHarmonyMethodUnity(Type originalClass, string originalMethodName, string patchedMethodName, bool usePrefix, bool usePostfix, Type[] parameters = null)
        {
            // Create a new Harmony instance with a unique ID
            var harmony = new Harmony("com.wolfitdm.BitchlandMoreWeaponSlotsBepInEx");

            if (originalClass == null)
            {
                Logger.LogInfo($"GetType originalClass == null");
                return;
            }

            // Or apply patches manually
            MethodInfo original = null;

            if (parameters == null)
            {
                original = AccessTools.Method(originalClass, originalMethodName);
            } else
            {
                original = AccessTools.Method(originalClass, originalMethodName, parameters);
            }

            if (original == null)
            {
                Logger.LogInfo($"AccessTool.Method original {originalMethodName} == null");
                return;
            }

            MethodInfo patched = AccessTools.Method(typeof(BitchlandMoreWeaponSlotsBepInEx), patchedMethodName);

            if (patched == null)
            {
                Logger.LogInfo($"AccessTool.Method patched {patchedMethodName} == null");
                return;

            }

            HarmonyMethod patchedMethod = new HarmonyMethod(patched);
			
            var prefixMethod = usePrefix ? patchedMethod : null;
            var postfixMethod = usePostfix ? patchedMethod : null;

            harmony.Patch(original,
                prefix: prefixMethod,
                postfix: postfixMethod);
        }

        public static bool WeaponSystem_SetActiveWeapon(object __instance, int index)
        {
            if (!enableMe)
            {
                return true;
            }

            WeaponSystem _this = (WeaponSystem)__instance;

            while (_this.weapons.Count < 10)
            {
                _this.weapons.Add(null);
            }

            if (index >= _this.weapons.Count || index < 0)
            {
                return true;
            }
            else
            {
                _this.SendMessageUpwards("OnEasyWeaponsSwitch", SendMessageOptions.DontRequireReceiver);
                if (_this.CurrentWeapon != null)
                    _this.CurrentWeapon.SetInRelax();
                _this.weaponIndex = index;
                for (int index1 = 1; index1 < _this.weapons.Count; ++index1)
                {
                    if (_this.weapons[index1] != null)
                        _this.weapons[index1].SetActive(false);
                }
                switch (index)
                {
                    default:
                        if (_this.weapons.Count >= 2 && _this.weapons[0] != null)
                            _this.weapons[0].GetComponent<Weapon>().SetInHoldster1();
                        if (_this.weapons.Count >= 3 && _this.weapons[1] != null)
                        {
                            _this.weapons[1].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 4 && _this.weapons[2] != null)
                        {
                            _this.weapons[2].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 5 && _this.weapons[3] != null)
                        {
                            _this.weapons[3].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 6 && _this.weapons[4] != null)
                        {
                            _this.weapons[4].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 7 && _this.weapons[5] != null)
                        {
                            _this.weapons[5].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 8 && _this.weapons[6] != null)
                        {
                            _this.weapons[6].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 9 && _this.weapons[7] != null)
                        {
                            _this.weapons[7].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        if (_this.weapons.Count >= 10 && _this.weapons[8] != null)
                        {
                            _this.weapons[8].GetComponent<Weapon>().SetInHoldster2();
                            break;
                        }
                        break;
                }
            }
            return true;
        }
		public static bool WeaponSystem_Update(object __instance)
        {
            if (!enableMe)
            {
                return true;
            }

            WeaponSystem _this = (WeaponSystem)__instance;

            while (_this.weapons.Count < 10)
            {
                _this.weapons.Add(null);
            }

            if (!_this.isPlayer || _this.ThisPerson.Interacting || !_this.ThisPerson.CanMove)
                return true;

            if (Input.GetKeyUp(KeyCodeSwitchWeapon1) && _this.weapons.Count > 0)
                _this.SetActiveWeapon(0);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon2) && _this.weapons.Count > 1)
                _this.SetActiveWeapon(1);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon3) && _this.weapons.Count > 2)
                _this.SetActiveWeapon(2);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon4) && _this.weapons.Count > 3)
                _this.SetActiveWeapon(3);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon5) && _this.weapons.Count > 4)
                _this.SetActiveWeapon(4);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon6) && _this.weapons.Count > 5)
                _this.SetActiveWeapon(5);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon7) && _this.weapons.Count > 6)
                _this.SetActiveWeapon(6);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon8) && _this.weapons.Count > 7)
                _this.SetActiveWeapon(7);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon9) && _this.weapons.Count > 8)
                _this.SetActiveWeapon(8);
            if (Input.GetKeyUp(KeyCodeSwitchWeapon10) && _this.weapons.Count > 9)
                _this.SetActiveWeapon(9);

            return true;
        }
    }
}