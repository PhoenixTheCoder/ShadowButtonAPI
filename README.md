# ShadowButtonAPI

### Very crappy ButtonAPI made by Foonix#0001
### Mostly inspired by EvilEye's ButtonAPI

## How to use:

```cs
// Creating a Tab Menu
// Params: (Menu name, Page Title, Tool Tip, Sprite)
QMTab mainTab = new QMTab("Menu Name", "Page Title", "Tool Tip", tabImage);

// Adding a button the Tab Menu
// Params: (Transform parent, "Button Text")
QMNestedButton menuBtn = new QMNestedButton(mainTab.menuTransform, "Menu Button");

// Adding a button inside of the Menu Button
// Params: (Transform parent, Button Title, Button Tool Tip, Sprite (null), Action)
new QMSingleButton(menuBtn, "Button Title", "Button Tool Tip", null, delegate {
  // Do something when button is pressed
});

// Or if you want to add a button image (Sprite)
// Params: (Transform parent, Button Title, Button Tool Tip, Sprite (Optional or null), Action)
new QMSingleButton(menuBtn, "Button Title", "Button Tool Tip", buttonImage, delegate {
  // Do something when button is pressed
});

// Adding a toggle button
// Params: (Transform parent, Button Title, Button Tool Tip, Action)
new QMToggleButton(menuBtn, "Button Title", "Button Tool Tip", (v) => {
  if (v) {
    // v is equal to true at this point
  } else {
    // v is no longer equal to true at this point
  }
});

// Adding a label
// Params: (Transform Parent, x, y, "Content")
new QMLabel(menuBtn, 1, 1, "Label Content");

// Adding a menu 
// Params: (Menu Name, Page Title, Root (Bool), Include back button (Bool))
new QMMenu("Menu Name", "Page Title", true, true);
```

## User Selection Buttons

```cs
// Adding a button to Select User Menu
// Params: (Button Title, Button Tool Tip, Sprite (Optional), Action)
new SUButton("Button Title", "Button Tool Tip", null, () => {
  // Do something when user button is pressed
});

// Adding a toggle button to Select User Menu
// Params: (Button Title, Button Tool Tip, Action)
new SUButton("Button Title", "Button Tool Tip", (v) => {
  if (v) {
    // Do something when toggle button is toggled on
  } else {
    // Do something when toggle button is toggled off
  }
});

// Adding a Label to Select User Menu
// Params: (x, y, "content")
new SULabel(1, 1, "Content");
```

### If you have any questions about this, join (https://discord.gg/Av39q7Aecg)[ShadowVRC's Discord / Foonix's Discord]
