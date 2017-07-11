FluentZpl
=========

This is an improved version of the original Version from agglerithm.

<h2>A fluent interface to build labels using ZPL</h2>

FluentZpl consists of an assembly called ZplLabels that allows creation and printing of Zebra labels through a fluent interface. The ZplLabel class enables creation of label scripts through its Load() method, and either PrinterConnection or LabelPrinter can be used to send the resulting script to a Zebra printer.

The ZplLabel.Load() method takes an array of IFieldGenerator objects as parameters.  The field generators are created through static methods of the ZplFactory class. 

<h2>Creating a text field:</h2>

<pre>FieldGenFactory.GetText().At(1, 500).SetFont("D", FieldOrientation.Normal, 56).WithData("PO Line Number").Centered(1200)
</pre>
<h2>Creating a barcode field:</h2>
<pre>
 FieldGenFactory.GetBarcode().At(1, 550).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 40).WithData("1").Height(70).BarWidth(2).Centered(1200) </pre>

<h2>Positions in milimeter instead of Pixel</h2>
<pre>
var dpi = new DPIHelper(600);
FieldGenFactory.GetText().At(dpi,10, 20).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 50).WithData("Testlabel")
</pre>
If you want to provide your Labelpositions in milimeter instead of Pixel you can user the ZplLabels.Utilities.DPIHelper class. First you need to create an DPIHelper Object with dpi value from your printer. Now you can use the At() methods with the DPIHelper object and the x and y positions in milimeters

<h2>Add your own ZPL Code to Label</h2>
<pre>label.customZPLCommand("^FO100,100^ADN,80^FDCustomZPL^FS")</pre>
Adds the Text CustomZPL to your Label. This function is useful to add your Company Logo as ZPL Code

<h2>Darkness</h2>
Set the Darkness of printermedia
<pre>label.Darkness(26.5)</pre>

<h2>Set Printer Mode</h2>
Most printers have special functions like cutter or Peel Off unit. It is possible to control these units through ZPL
<pre>label.Mode(PrintMode.cut)</pre>

The following modes are avialable
* continous
* cut
* peelOff
There are some other modes avialable. Feel free to add these to the library

<h2>To create a complete label:</h2>

<pre>var _label = new ZplLabel();
var label = _label.Load(    
  FieldGenFactory.GetBarcode().SetBarcodeType(BarcodeType.DataMatrix).printTextLabel(false).Height(14).WithData("UI123456789").At(827, 307),    
  FieldGenFactory.GetText().At(850, 24).SetFont(ZplFonts.D, FieldOrientation.Normal, 40).WithData("Testlabel"),    
  FieldGenFactory.GetText().At(71, 118).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 50).WithData("Testlabel Testlabel Testlabel"),    
  FieldGenFactory.GetText().At(47, 614).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 70).WithData("UID: Testlabel"),    
  FieldGenFactory.GetText().At(47, 496).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 60).WithData("Reel: Testlabel"),    
  FieldGenFactory.GetText().At(47, 378).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 60).WithData("MSL: Testlabel"),    
  FieldGenFactory.GetText().At(47, 260).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 60).WithData("Menge: Testlabel"),    
  FieldGenFactory.GetText().At(71, 24).SetFont(ZplFonts.ZERO, FieldOrientation.Normal, 70).WithData("SAP: Testlabel")    
).At(0, 0).customZPLCommand("^FO100,100^ADN,80^FDCustomZPL^FS").CutOffset(0).Mode(PrintMode.cut);

var zplCode = label.ToString();
</pre>
                
The "ToString()" method returns the ZPL Code as String

<h2>Networkprinter</h2>
<pre>
var result = (new PrinterConnection()).Print(zplCode, printername);
if (result != "OK")
{
    logger.Error(result);
    MessageBox.Show(result);
}
</pre>
You can either provide the IP Adress or the printername if you use dns Server
