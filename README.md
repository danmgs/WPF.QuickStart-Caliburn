# WPF.QuickStart Caliburn - 2014

*Designed by [@Daniel NGUYEN](https://www.linkedin.com/in/nguyendaniel)* :smirk_cat:

WPF quickstart caliburn is a ready-to-use project that uses :

- [WCF Services](http://msdn.microsoft.com/fr-fr/library/dd456779%28v=vs.110%29.aspx) *for Client/Server communication in duplex*
- [MahApps.Metro](http://mahapps.com/) *for UI design*
- [Caliburn.Micro](https://github.com/Caliburn-Micro/Caliburn.Micro) *to setup MVVM*
- [Twitter API](https://dev.twitter.com/)
- [Yahoo! Finance API](https://code.google.com/p/yahoo-finance-managed/wiki/YahooFinanceAPIs)

##INSTALLATION NOTES

###Server side Console: 

Launch the server side project executable.

###Client Side UI:

Launch the client.

:heavy_exclamation_mark: *In order to be allowed connection and retrieved data, the server console must be started.*

:bulb: *Under VS Debug Mode, you can start multiple projects WPF.QuickStart.UI & WPF.QuickStart.Server*

##USEFUL LINKS:

###WPF FAQ 

> ####WPF Layout :

>> [Dockpanel](http://wpftutorial.net/DockPanel.html)
*The order of element added is important, last element will filled the remaining space, when LastFillChild = true*

>> [Grid & Overlay](http://stackoverflow.com/questions/5450985/how-to-make-overlay-control-above-all-other-controls)
*Controls in the same cell of a Grid are rendered back-to-front*

> ####WPF Styling :

>> http://www.wpf-tutorial.com/styles/using-styles/

> ####WPF Binding :

>> [How do I use WPF bindings with RelativeSource?](http://stackoverflow.com/questions/84278/how-do-i-use-wpf-bindings-with-relativesource)

> ####WPF Controls :

>> http://wpftutorial.net/DataGrid.html>

> ####WPF UI Freezing FAQ :

>> http://stackoverflow.com/questions/10065000/running-long-tasks-without-freezing-the-ui

>> http://blog.stephencleary.com/2012/02/async-and-await.html


###TWITTER :

[oAuthTwitterTimeline](https://github.com/marcemarc/oAuthTwitterTimeline)


###CALIBURN.MICRO :

[All about actions](https://caliburnmicro.codeplex.com/wikipage?title=All%20About%20Actions)

[Customizing bootstrapper](https://caliburnmicro.codeplex.com/wikipage?title=Customizing%20The%20Bootstrapper)


###WPF LIBS ON GITHUB/CODEPLEX :

[wpftoolkit](https://wpftoolkit.codeplex.com/)

[modernuicharts](https://modernuicharts.codeplex.com)

[oxyplot](https://github.com/oxyplot/oxyplot)

[wpfanimatedgif](http://wpfanimatedgif.codeplex.com/)

[mahapps](http://mahapps.com/) *Great UI Styles*


###OTHER TOOLS :

####Json

> [json2csharp Generate class from json](http://json2csharp.com/)

> http://stackoverflow.com/questions/21611674/how-to-auto-generate-a-c-sharp-class-file-from-a-json-object-string

http://stackoverflow.com/questions/833943/watermark-hint-text-placeholder-textbox-in-wpf

[xaml Spy](http://xamlspy.com/)


##ENCOUNTERED ISSUES :

Pour les problèmes d'import dans le XAML de namespaces non reconnu lorsque les dll proviennent du WEB :

https://wpfanimatedgif.codeplex.com/discussions/392120

Par ordre de priorité :

1. **Si disponible, installer la dll via nugget manager.**

2. **Ou bien, aller dans les propriétés de la dll => "Unblock"/"Débloquer".**

3. **Ou bien, ajouter dans le .config du projet** :

```html
  <runtime>
    <loadFromRemoteSources enabled="true" />
  </runtime>
```

##TASKS LIST 

- [:construction_worker:] Setup Data layer with Entity Framework instead of using mock repositories.
- [:construction_worker:] Inject IOC for Services using MEF
- [:construction_worker:] Find a way to flash the row when an item is an in a listbox (event/trigger ..)
- [:construction_worker:] Implement a DialogWindow with a webbrowser when clicking on the top bar on Twitter/Yahoo.

##KNOWN BUGS :

- [:scream_cat:] Close WCF Service OnDeactivate ChildViewModel (i.e stop twitter from pushing data automatically).
- [:scream_cat:] Client Server Screen : fix bug when get tweets then query server. Cannot call the snd operation because same channel is still open to callback the tweets.
- [:scream_cat:] Screen Display Name not working
