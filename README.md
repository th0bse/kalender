# Kalender
This is a small calendar app written in C# with WPF as UI-Framework.
It is a school project for the programming class at BBSII Göttingen.

To run the project, .net 6.0 is needed.

## Structure
There are three projects in the solution:
* CalendarLib
* CalendarCLI
* CalendarUI

**CalendarLib** is the common library for both *CalendarCLI* and 
*CalendarUI*. It contains methods for calculating easter, for
calculating the weekday for a given day and also several enums that
represent things like months or holidays. It also contains methods to
work with those enums.

**CalendarCLI** is a very basic command line interface that prints a
calendar sheet for a given month and also gives all the holidays in
that month. It is mainly meant to be a simple testing device for the
library, and therefore contains most but not all functionality that is
present in the *CalendarUI* project.

**CalendarUI** is a GUI that uses the WPF framework. It is a calendar
app that displays a month of choice, ranging from October 1582 to
December 3000. It also uses the functionality provided by *CalendarLib*.

The required program structure diagrams are in the folder **images**.