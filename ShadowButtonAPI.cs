using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnhollowerRuntimeLib.XrefScans;
using System.Reflection;
using VRC.UI.Elements.Controls;
using VRC.UI.Core.Styles;

// Inspired by EvilEye's ButtonAPI
// Changed and added Selected User Buttons

namespace ShadowButtonAPI
{
	public class QMLabel
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public TextMeshProUGUI text;
		public GameObject label;
		public QMLabel(Transform menu, float x, float y, string contents)
		{
			label = UnityEngine.Object.Instantiate<GameObject>(qmStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks").gameObject, menu);
			label.name = contents;
			label.transform.localPosition = new Vector3(x, y, 0);
			text = label.GetComponentInChildren<TextMeshProUGUI>();
			text.text = contents;
			text.enableAutoSizing = true;
			text.color = Color.white;
			label.gameObject.SetActive(false);
		}
	}

	public class QMMenu
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public UIPage page;
		public Transform menuContents;

		public QMMenu(string menuName, string pageTitle, bool root = true, bool backButton = true)
		{
			GameObject menu = UnityEngine.Object.Instantiate<GameObject>(qmStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools").gameObject, qmStuff.quickMenu.transform.Find("Container/Window/QMParent"));
			menu.name = "Menu_" + menuName;
			menu.transform.SetSiblingIndex(5);
			menu.SetActive(false);

			UnityEngine.Object.Destroy(menu.GetComponent<DevMenu>());

			page = menu.AddComponent<UIPage>();
			page.field_Public_String_0 = menuName;
			page.field_Private_Boolean_1 = true;
			page.field_Protected_MenuStateController_0 = qmStuff.menuStateController;
			page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
			page.field_Private_List_1_UIPage_0.Add(page);
			if (!root)
			{
				page.field_Public_Boolean_0 = true;
				try
				{
					menu.transform.Find("Scrollrect/Scrollbar").gameObject.SetActive(true);
					menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().enabled = true;
					menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbar = menu.transform.Find("Scrollrect/Scrollbar").GetComponent<Scrollbar>();
					menu.transform.Find("Scrollrect").GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
				}
				catch { }
			}
			qmStuff.menuStateController.field_Private_Dictionary_2_String_UIPage_0.Add(menuName, page);
			if (root)
			{
				System.Collections.Generic.List<UIPage> list = qmStuff.menuStateController.field_Public_ArrayOf_UIPage_0.ToList<UIPage>();
				list.Add(page);
				qmStuff.menuStateController.field_Public_ArrayOf_UIPage_0 = list.ToArray();
			}
			TextMeshProUGUI pageTitleText = menu.GetComponentInChildren<TextMeshProUGUI>(true);
			pageTitleText.text = pageTitle;
			menuContents = menu.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
			for (int i = 0; i < menuContents.transform.childCount; i++)
				GameObject.Destroy(menuContents.transform.GetChild(i).gameObject);
			if (backButton)
			{
				GameObject backButtonGameObject = menu.transform.Find("Header_DevTools/LeftItemContainer/Button_Back").gameObject;
				backButtonGameObject.SetActive(true);
				backButtonGameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
				backButtonGameObject.GetComponent<Button>().onClick.AddListener(new System.Action(() =>
				{
					page.Method_Protected_Virtual_New_Void_0();
				}));
			}
		}

		public void OpenMenu()
		{
			qmStuff.menuStateController.Method_Public_Void_String_UIContext_Boolean_0(this.page.field_Public_String_0, null, false);
		}

		public void CloseMenu()
		{
			this.page.Method_Public_Virtual_New_Void_0();
		}
	}

	public class QMNestedButton
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public QMMenu menu;
		public Transform menuTransform;

		public QMNestedButton(Transform perant, string name, Sprite icon = null)
		{
			menu = new QMMenu(name, name, false, true);
			menuTransform = menu.menuContents;

			new QMSingleButton(perant, name, name, icon, delegate
			{
				menu.OpenMenu();
			});
		}
	}
	public static class QMPopup
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public static void HideCurrentPopup(this VRCUiPopupManager vrcUiPopupManager)
		{
			//VRCUiManager.prop_VRCUiManager_0.HideScreen("POPUP");
		}
		public static void ShowStandardPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string content, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopup1(title, content, onCreated);
		}
		public static void ShowStandardPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string buttonText, System.Action buttonAction, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopup2(title, content, buttonText, buttonAction, onCreated);
		}
		public static void ShowStandardPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string button1Text, System.Action button1Action, string button2Text, System.Action button2Action, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopup3(title, content, button1Text, button1Action, button2Text, button2Action, onCreated);
		}
		public static void ShowStandardPopupV2(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string buttonText, System.Action buttonAction, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopupV21(title, content, buttonText, buttonAction, onCreated);
		}
		public static void ShowStandardPopupV2(this VRCUiPopupManager vrcUiPopupManager, string title, string content, string button1Text, System.Action button1Action, string button2Text, System.Action button2Action, System.Action<VRCUiPopup> onCreated = null)
		{
			QMPopup.ShowUiStandardPopupV22(title, content, button1Text, button1Action, button2Text, button2Action, onCreated);
		}
		public static void ShowInputPopup(this VRCUiPopupManager vrcUiPopupManager, string title, string preFilledText, InputField.InputType inputType, bool keypad, string buttonText, Il2CppSystem.Action<string, System.Collections.Generic.List<KeyCode>, Text> buttonAction, Il2CppSystem.Action cancelAction, string boxText = "Enter text....", bool closeOnAccept = true, System.Action<VRCUiPopup> onCreated = null, bool startOnLeft = false, int characterLimit = 0)
		{
			QMPopup.ShowUiInputPopup(title, preFilledText, inputType, keypad, buttonText, buttonAction, cancelAction, boxText, closeOnAccept, onCreated, startOnLeft, characterLimit);
		}
		public static void ShowAlert(this VRCUiPopupManager vrcUiPopupManager, string title, string content, float timeout)
		{
			QMPopup.ShowUiAlertPopup(title, content, timeout);
		}
		public static QMPopup.ShowUiInputPopupAction ShowUiInputPopup
		{
			get
			{
				if (QMPopup.ourShowUiInputPopupAction != null)
				{
					return QMPopup.ourShowUiInputPopupAction;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 12)
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/InputPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiInputPopupAction = (QMPopup.ShowUiInputPopupAction)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiInputPopupAction), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiInputPopupAction;
			}
		}
		public static QMPopup.ShowUiStandardPopup1Action ShowUiStandardPopup1
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopup1Action != null)
				{
					return QMPopup.ourShowUiStandardPopup1Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 3 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopup1Action = (QMPopup.ShowUiStandardPopup1Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopup1Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopup1Action;
			}
		}
		public static QMPopup.ShowUiStandardPopup2Action ShowUiStandardPopup2
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopup2Action != null)
				{
					return QMPopup.ourShowUiStandardPopup2Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 5 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopup2Action = (QMPopup.ShowUiStandardPopup2Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopup2Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopup2Action;
			}
		}

		public static QMPopup.ShowUiStandardPopup3Action ShowUiStandardPopup3
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopup3Action != null)
				{
					return QMPopup.ourShowUiStandardPopup3Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 7 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopup3Action = (QMPopup.ShowUiStandardPopup3Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopup3Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopup3Action;
			}
		}
		public static QMPopup.ShowUiStandardPopupV21Action ShowUiStandardPopupV21
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopupV21Action != null)
				{
					return QMPopup.ourShowUiStandardPopupV21Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 5 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopupV2";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopupV21Action = (QMPopup.ShowUiStandardPopupV21Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopupV21Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopupV21Action;
			}
		}
		public static QMPopup.ShowUiStandardPopupV22Action ShowUiStandardPopupV22
		{
			get
			{
				if (QMPopup.ourShowUiStandardPopupV22Action != null)
				{
					return QMPopup.ourShowUiStandardPopupV22Action;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 7 && !it.Name.Contains("PDM"))
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/StandardPopupV2";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiStandardPopupV22Action = (QMPopup.ShowUiStandardPopupV22Action)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiStandardPopupV22Action), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiStandardPopupV22Action;
			}
		}
		public static QMPopup.ShowUiAlertPopupAction ShowUiAlertPopup
		{
			get
			{
				if (QMPopup.ourShowUiAlertPopupAction != null)
				{
					return QMPopup.ourShowUiAlertPopupAction;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(delegate (MethodInfo it)
				{
					if (it.GetParameters().Length == 3)
					{
						return XrefScanner.XrefScan(it).Any(delegate (XrefInstance jt)
						{
							if (jt.Type == XrefType.Global)
							{
								Il2CppSystem.Object @object = jt.ReadAsObject();
								return ((@object != null) ? @object.ToString() : null) == "UserInterface/MenuContent/Popups/AlertPopup";
							}
							return false;
						});
					}
					return false;
				});
				QMPopup.ourShowUiAlertPopupAction = (QMPopup.ShowUiAlertPopupAction)System.Delegate.CreateDelegate(typeof(QMPopup.ShowUiAlertPopupAction), VRCUiPopupManager.prop_VRCUiPopupManager_0, method);
				return QMPopup.ourShowUiAlertPopupAction;
			}
		}

		private static QMPopup.ShowUiInputPopupAction ourShowUiInputPopupAction;
		private static QMPopup.ShowUiStandardPopup1Action ourShowUiStandardPopup1Action;
		private static QMPopup.ShowUiStandardPopup2Action ourShowUiStandardPopup2Action;
		private static QMPopup.ShowUiStandardPopup3Action ourShowUiStandardPopup3Action;
		private static QMPopup.ShowUiStandardPopupV21Action ourShowUiStandardPopupV21Action;
		private static QMPopup.ShowUiStandardPopupV22Action ourShowUiStandardPopupV22Action;
		private static QMPopup.ShowUiAlertPopupAction ourShowUiAlertPopupAction;
		public delegate void ShowUiInputPopupAction(string title, string initialText, InputField.InputType inputType, bool isNumeric, string confirmButtonText, Il2CppSystem.Action<string, System.Collections.Generic.List<KeyCode>, Text> onComplete, Il2CppSystem.Action onCancel, string placeholderText = "Enter text...", bool closeAfterInput = true, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null, bool startOnLeft = false, int characterLimit = 0);
		public delegate void ShowUiStandardPopup1Action(string title, string body, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopup2Action(string title, string body, string middleButtonText, Il2CppSystem.Action middleButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopup3Action(string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopupV21Action(string title, string body, string middleButtonText, Il2CppSystem.Action middleButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiStandardPopupV22Action(string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction, Il2CppSystem.Action<VRCUiPopup> onPopupShown = null);
		public delegate void ShowUiAlertPopupAction(string title, string body, float timeout);
	}
	public class QMSingleButton
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public QMSingleButton(Transform parent, string text, string toolTip, Sprite Icon, System.Action action)
		{
			GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(qmStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Emojis").gameObject, parent);
			singleButton.transform.parent = parent;
			singleButton.name = text + "_Shadow_Button";

			singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
			if (Icon != null)
			{
				singleButton.transform.Find("Icon").GetComponent<Image>().sprite = Icon;
			}
			else
			{
				UnityEngine.Object.Destroy(singleButton.transform.Find("Icon").GetComponent<Image>());
			}
			singleButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = toolTip;
			Button button = singleButton.GetComponent<Button>();
			button.onClick = new Button.ButtonClickedEvent();
			button.onClick.AddListener(action);
			singleButton.SetActive(true);
		}
	}

	class QMTab
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public QMMenu menu;
		public Transform menuTransform;

		public QMTab(string menuName, string pagetitle, string tooltip, Sprite icon = null)
		{
			menu = new QMMenu(menuName, pagetitle, true, false);
			menuTransform = menu.menuContents;

			GameObject tab = UnityEngine.Object.Instantiate<GameObject>(qmStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools").gameObject, qmStuff.quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup"));
			tab.name = menuName + "Tab";
			MenuTab menuTab = tab.GetComponent<MenuTab>();
			menuTab.field_Private_MenuStateController_0 = qmStuff.menuStateController;
			menuTab.field_Public_String_0 = menuName;

			Image tabImage = tab.transform.Find("Icon").GetComponent<Image>();
			tab.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = tooltip;
			tabImage.sprite = icon;
			tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
			tab.GetComponent<Button>().onClick.AddListener(new System.Action(() =>
			{
				tab.GetComponent<StyleElement>().field_Private_Selectable_0 = tab.GetComponent<Button>();
			}));
			tab.SetActive(true);
		}
	}

	public class QMToggleButton
	{
		public static QuickMenuStuff qmStuff = new QuickMenuStuff();

		public Toggle toggleButton;

		public QMToggleButton(Transform parent, string text, string toolTip, System.Action<bool> action)
		{
			GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(qmStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject, parent);
			singleButton.transform.parent = parent;
			singleButton.name = text + "_Shadow_ToggleButton";

			singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
			singleButton.transform.Find("Icon_On").GetComponent<Image>().sprite = qmStuff.Panel_NoNotifications_MessageIcon.sprite;
			singleButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = toolTip;
			toggleButton = singleButton.GetComponent<Toggle>();
			toggleButton.onValueChanged = new Toggle.ToggleEvent();
			toggleButton.onValueChanged.AddListener(action);
			singleButton.SetActive(true);
		}
	}

	public class QuickMenuStuff
	{
		public Image Button_WorldsIcon;
		public Image Button_AvatarsIcon;
		public Image Button_SocialIcon;
		public Image Button_SafetyIcon;
		public Image Panel_NoNotifications_MessageIcon;

		public Image Button_NameplateVisibleIcon;

		public Image Button_GoHomeIcon;
		public Image Button_RespawnIcon;
		public Image StandIcon;

		public VRC.UI.Elements.QuickMenu quickMenu;
		public SelectedUserMenuQM selectedUserMenuQM;
		public MenuStateController menuStateController;

		public Transform tabMenuTemplat;
		public Transform Menu_DevTools;
		public Transform Menu_Dashboard;
		public Transform QMParent;
		public Transform page_Buttons_QM;

		public QuickMenuStuff()
		{
			quickMenu = Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().First();
			menuStateController = quickMenu.gameObject.GetComponent<MenuStateController>();

			Button_WorldsIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Worlds/Icon").GetComponent<Image>();
			Button_AvatarsIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars/Icon").GetComponent<Image>();
			Button_SocialIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Social/Icon").GetComponent<Image>();
			Button_SafetyIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Safety/Icon").GetComponent<Image>();

			Button_GoHomeIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome/Icon").GetComponent<Image>();
			Button_RespawnIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn/Icon").GetComponent<Image>();
			StandIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/SitStandCalibrateButton/Button_SitStand/Icon_Off").GetComponent<Image>();
			Panel_NoNotifications_MessageIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").gameObject.GetComponent<Image>();

			Button_NameplateVisibleIcon = quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_NameplateControls/Buttons/Button A/Icon").GetComponent<Image>();

			selectedUserMenuQM = quickMenu.transform.Find("Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
			tabMenuTemplat = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools");
			Menu_DevTools = quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools");
			QMParent = quickMenu.transform.Find("Container/Window/QMParent");
			page_Buttons_QM = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");
			Menu_Dashboard = quickMenu.transform.Find("Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings");
		}
	}

	public class SUButton 
	{
		QuickMenuStuff qmStuff = new QuickMenuStuff();
		public SUButton(string text, string toolTip, Sprite sprite, System.Action action)
        {
			new QMSingleButton(qmStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, text, toolTip, sprite, action);
        }
	}

	public class SUToggleButton
	{
		QuickMenuStuff qmStuff = new QuickMenuStuff();
		public SUToggleButton(string text, string toolTip, System.Action<bool> action)
		{
			new QMToggleButton(qmStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, text, toolTip, action);
		}
	}

	public class SULabel
	{
		QuickMenuStuff qmStuff = new QuickMenuStuff();
		public SULabel(float x, float y, string content)
		{
			new QMLabel(qmStuff.selectedUserMenuQM.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").transform, x, y, content);
		}
	}
}
