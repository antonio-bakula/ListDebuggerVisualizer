# Visual Studio List Debugger Visualizer
I allways find inspecting list in the debugger pretty tedious and difficult, so I decided to change that :)

I made a Visual Studio debugger visualizer for List<T> or more precisly if inspected object implement IList, downside is that type T must have Serializable attribute, but that is a requirement for debugger visualizer and only Microsoft can change that.

I used Telerik RadGridView for displaying data because it's packed with nice features and I was done in no time, but downside is that visualizer dll is 4MB and sometimes can be slow to show. Another slick feature is that your visualizer will remember grid settings for every type separately, and there is a handy option to export visualized data to Excel.

### Binaries download:

http://www.antoniob.com/visual-studio-list-debugger-visualizer.html

### Installation:
Simply copy visualizer dll to your Visual Studio Visualizers folder, default folder is:

* VS 2017 -> c:\Users\<username>\Documents\Visual Studio 2017\Visualizers
* VS 2015 -> c:\Users\<username>\Documents\Visual Studio 2015\Visualizers
* VS 2013 -> c:\Users\<username>\Documents\Visual Studio 2013\Visualizers
* VS 2012 -> c:\Users\<username>\Documents\Visual Studio 2012\Visualizers
