using System;
using System.Collections.Generic;

namespace NAUL.Manager;

public class PageControl
{
    public Type PageType { get => PageClass.GetType(); }

    public object PageClass { get; set; }
    
    public string PageName { get; set; }

    public string PageIcon { get; set; }

    public MenuTypes MenuType { get; set; }

    public void Show(bool needChangeNaviSelection = true)
        => Page_Main.Current.NavigateTo(PageType, needChangeNaviSelection);

    public static List<PageControl> AllPages;

    public static void Init() => AllPages = new()
        {
            new()
            {
                PageClass = new Page_Play(),
                MenuType = MenuTypes.MainMenu,
                PageName = "启动游戏",
                PageIcon = "\uE768",
            },
            new()
            {
                PageClass = new Page_Version(),
                MenuType = MenuTypes.MainMenu,
                PageName = "版本管理",
                PageIcon = "\uE8EC",
            },
            new()
            {
                PageClass = new Page_Plugin(),
                MenuType = MenuTypes.MainMenu,
                PageName = "模组管理",
                PageIcon = "\uE8F1",
            },
            new()
            {
                PageClass = new Page_About(),
                MenuType = MenuTypes.FooterMenu,
                PageName = "关于",
                PageIcon = "\uE779",
            },
            new()
            {
                PageClass = new Page_Setting(),
                MenuType = MenuTypes.FooterMenu,
                PageName = "设置",
                PageIcon = "\uE713",
            },
        };

    public static void NavigateTo(Type type)
        => AllPages.Find(p => p.PageType == type)?.Show();

    public static void NavigateTo(object page)
        => AllPages.Find(p => p.PageClass == page)?.Show();
}

public enum MenuTypes
{
    MainMenu,
    FooterMenu,
}