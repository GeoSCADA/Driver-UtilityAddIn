Please refer to the file LICENSE.txt for terms relating to this code

This is code for a collection of utility point functions for Geo SCADA 
Expert, written in C# and VB for the Simple Driver Framework.

IMPORTANT

The driver is offered as source code which you can build with Visual 
Studio. It includes the two parts of the driver enabling you to build 
a package to deploy to Geo SCADA Expert servers. It is not supported.

The source code is available for you to freely use, modify and extend to 
suit your requirements or that of your clients. It is perhaps not the 
most optimized, efficient or elegant code but we hope that its simplicity 
will encourage engagement with the Geo SCADA driver development process 
and explore the ideas presented here.

To implement and deploy this example you will need to add appropriate 
security measures for your environment.

You can discuss these features and driver development in the SE Exchange 
forums.
https://community.exchange.se.com/t5/Geo-SCADA-Expert-Forum/bd-p/ecostruxure-geo-scada-expert-forum

DESCRIPTION OF FEATURES

1. Addin Scanner
Create this object first. You'll need it for all of the other object types.
The scan rate and offset are only needed for the Tariff Analogue

2. Manual Analogue
This point type is very much like an Internal Analogue point. However, the
key difference is that to change it's value you call a single Method to
pass in the point's value, quality and time of change. Another difference
is that if your timestamp is earlier than the latest process time of the
point, then the method will add/modify historic data instead.

3. Tariff Analogue
This point offers an easy way to look up the cost of (say) an electricity
tariff by reading the value from a table. The table allows up to 24 separate
month ranges, day types and time ranges. For example, you could configure
the point with a January-March value for weekdays during 8AM to 5PM.

The actual tariff/cost value is calculated at the scanner interval and
processed as the point value. There is also a Method on the object, which
you can call from Logic, to get the value at any time/date.

4. 2D Lookup
A 2D lookup is a pair of arrays of values of the same size. You can get the
Y value from any given X value. Methods enable you to enter and read values
and also import the X/Y data from a file.

5. 3D Lookup
The 3D lookup extends the 2D concept further, allowing a value to be
selected from a 'surface' derived from a array of XY values with corresponding
Z values for each X/Y combination. Again, there are Methods to support adding
data, and importing it from files.



