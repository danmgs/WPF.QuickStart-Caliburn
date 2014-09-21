# WPF.QuickStart Caliburn - 2014

*Designed by [@Daniel NGUYEN](https://www.linkedin.com/in/nguyendaniel)* :+1:

If you are looking for WPF quickstart project, designed with [Caliburn.Micro](https://github.com/Caliburn-Micro) ...

##Installation Notes

###Server side Console: 

Launch the server side project executable.

###Client Side UI:

Launch the client.
In order to be allowed connection and retrieved data, the server console must be started.


##Useful links :

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

> ####WPF UI Freezing FAQ :

>> http://stackoverflow.com/questions/10065000/running-long-tasks-without-freezing-the-ui

>> http://blog.stephencleary.com/2012/02/async-and-await.html


###Twitter :

[oAuthTwitterTimeline](https://github.com/marcemarc/oAuthTwitterTimeline)


###Caliburn :

[All about actions](https://caliburnmicro.codeplex.com/wikipage?title=All%20About%20Actions)

[Customizing bootstrapper](https://caliburnmicro.codeplex.com/wikipage?title=Customizing%20The%20Bootstrapper)


###WPF library on github/codeplex :

[wpftoolkit](https://wpftoolkit.codeplex.com/)

[modernuicharts](https://modernuicharts.codeplex.com)

[oxyplot](https://github.com/oxyplot/oxyplot)

[wpfanimatedgif](http://wpfanimatedgif.codeplex.com/)

[mahapps](http://mahapps.com/) *Great UI Styles*


###Others/library/tools :

####Json

> [json2csharp Generate class from json](http://json2csharp.com/)

> http://stackoverflow.com/questions/21611674/how-to-auto-generate-a-c-sharp-class-file-from-a-json-object-string

http://stackoverflow.com/questions/833943/watermark-hint-text-placeholder-textbox-in-wpf

[xaml Spy](http://xamlspy.com/)


##Problème rencontré de dll en provenance du WEB :

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

##TODO  

- [:construction_worker:] Setup Data layer with Entity Framework instead of using mock repositories.
- [:construction_worker: ] Inject IOC for Services using MEF
- [:construction_worker: ] Find a way to flash the row when an item is an in a listbox (event/trigger ..)
