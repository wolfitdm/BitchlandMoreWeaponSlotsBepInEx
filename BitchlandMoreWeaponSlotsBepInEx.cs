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

		public static bool enableMe = false;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableMe",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");
            
			enableMe = configEnableMe.Value;
			
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

            while (_this.weapons.Count < 9)
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

            while (_this.weapons.Count < 9)
            {
                _this.weapons.Add(null);
            }

            if (!_this.isPlayer || _this.ThisPerson.Interacting || !_this.ThisPerson.CanMove)
                return true;

            if (Input.GetKeyUp(KeyCode.Alpha4) && _this.weapons.Count > 3)
                _this.SetActiveWeapon(3);
            if (Input.GetKeyUp(KeyCode.Alpha5) && _this.weapons.Count > 4)
                _this.SetActiveWeapon(4);
            if (Input.GetKeyUp(KeyCode.Alpha6) && _this.weapons.Count > 5)
                _this.SetActiveWeapon(5);
            if (Input.GetKeyUp(KeyCode.Alpha7) && _this.weapons.Count > 6)
                _this.SetActiveWeapon(6);
            if (Input.GetKeyUp(KeyCode.Alpha8) && _this.weapons.Count > 7)
                _this.SetActiveWeapon(7);
            if (Input.GetKeyUp(KeyCode.Alpha9) && _this.weapons.Count > 8)
                _this.SetActiveWeapon(8);

            return true;
        }
    }
}