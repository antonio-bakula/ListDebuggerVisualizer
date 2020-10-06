# Visual Studio List Debugger Visualizer
I allways find inspecting list in the debugger pretty tedious and difficult, so I decided to change that :)

I made a Visual Studio debugger visualizer for List<T> or more precisly if inspected object implement IList.

I used Telerik RadGridView for displaying data because it's packed with nice features and I was done in no time, but downside is that visualizer dll is 4MB and sometimes can be slow to show. Another slick feature is that your visualizer will remember grid settings for every type separately, and there is a handy option to export visualized data to Excel.

### Binaries download:

http://www.antoniob.com/visual-studio-list-debugger-visualizer.html
Or see GitHub releases for this project.

### Installation:
Simply copy visualizer dll to your Visual Studio Visualizers folder, default folder is:

* VS 2019 -> c:\Users\<username>\Documents\Visual Studio 2019\Visualizers
* VS 2017 -> c:\Users\<username>\Documents\Visual Studio 2017\Visualizers


### Usage:
Types that implement IList will get little magnifier icon in Debugger  Locals / Variables Watch, like this:
![alt text](http://www.antoniob.com/EasyEdit/UserFiles/ListVisualizer/list-visualizer-usage.png "Visual Studio List Debugger Visualizer usage")

Click on magnifier icon and if IList members are marked as Serializable, their contents will be shown in grid, like on screenshots below.

### Screenshots:

![alt text](http://www.antoniob.com/EasyEdit/UserFiles/Slider/visual-studio-list-debugger-visualizer/visual-studio-list-debugger-visualizer-636236521545443093_800_450.jpeg "Visual Studio List Debugger Visualizer Screenshot 1")
![alt text](http://www.antoniob.com/EasyEdit/UserFiles/Slider/visual-studio-list-debugger-visualizer/visual-studio-list-debugger-visualizer-636236521547943121_800_450.jpeg "Visual Studio List Debugger Visualizer Screenshot 2")
![alt text](http://www.antoniob.com/EasyEdit/UserFiles/Slider/visual-studio-list-debugger-visualizer/visual-studio-list-debugger-visualizer-636236521551380600_800_450.jpeg "Visual Studio List Debugger Visualizer Screenshot 3")

