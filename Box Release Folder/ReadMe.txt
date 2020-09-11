AddIn Driver for ClearSCADA
===========================

These files are a ClearSCADA Driver which provides various point and object types for a variety of applications.

a) A 'Manual Analogue'
This is a point - similar to an Internal Analogue, but it has a single Method 'Enter Value' which causes a value to be processed in a similar way to the Internal Analogue, but it directly takes parameters of Time stamp, Quality and Reason, which are applied when the method is processed. If the point has a process time later than the Time stamp entered, then the historic value is entered. This point must be linked to an AddIn Scanner which is required for it to work.

b) A 'Tariff Analogue'
This is a point, which requires the AddIn Scanner as above. It can be configured with various properties which enable it to represent the cost of a resource throughout the year - by month, type of day and time. If the scanner is configured with a 'scan' rate, then the Tariff Analogue will have the tariff value at that rate. There is a Method which can be executed in order to read the tariff value at a specific date.

c) A 2D Lookup
This standalone object has various methods which enable the lookup of a list of X-Y values - to obtain Y from X, similar to the Excel VLOOKUP function. The configuration property is the interpolation type - Interpolated (linear), Step First or Step Last. Methods enable the insertion of X values and Y values as comma-separated lists, or import from a text file (CSV) on the server. Right-click the object to see these, or see the schema. The method 'LookupYfromX( x-value)' can be used in Logic or mimics, and will calculate the Y value. Currently the X and Y data in the object is saved to the database as 'data' and not configuration, so will not be exported with an SDE file. 

d) A 3D Lookup
This object extends the above to a lookup of a Z value from X and Y values and a two-dimensional array of Z values. X and Y values need not be evenly spaced, and can be set separately via database methods. See the schema for further details. Again, the interpolation can be linear or stepped, and data can be entered directly via methods or using methods which import data from files on the server. Currently the X, Y and Z data in the object is saved to the database as 'data' and not configuration, so will not be exported with an SDE file.

Please use the support forum for any queries, e.g, to report issues or discuss features:
http://telemetry.schneider-electric.com/id3/forum/index.cfm?forumid=5
