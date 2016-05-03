(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/Endorphin.Instrument.Twickenham.MagnetController/"

#r "Endorphin.Core"
#r "Endorphin.Core.NationalInstruments"
#r "Endorphin.Instrument.Twickenham.MagnetController.dll"

open System
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols
open Endorphin.Instrument.Twickenham.MagnetController

let settings = 
    { MagnetController.HardwareParameters =
        { MaximumCurrent = 20.0M<A>
          CalibratedRampRates =
            [ 0.020;   0.024;   0.026;   0.030;   0.036;   0.042;   0.048;   0.054; 
              0.064;   0.072;   0.084;   0.098;   0.110;   0.130;   0.150;   0.170;
              0.20;    0.24;    0.26;    0.30;    0.36;    0.42;    0.48;    0.54; 
              0.64;    0.72;    0.84;    0.98;    1.10;    1.30;    1.50;    1.70; 
              2.0 ]
            |> List.map (fun x -> (decimal x) * 1.0M<A/s>) }
              
      MagnetController.Limits =
        { RampRateLimit    = 0.1M<A/s>
          TripVoltageLimit = 2.5M<V>
          CurrentLimit     = 17.5M<A> }
          
      MagnetController.FieldCalibration =
        { StaticField       = 14.146M<T>
          LinearCoefficient = -0.002845M<T/A> }
          
      MagnetController.ShuntCalibration = 
        { VoltageOffset     = 0.002M<V>
          LinearCoefficient = 0.400M<V/A> 
          RmsVoltageNoise   = 0.100M<V> }
      
      MagnetController.LastUpdated = new DateTime(2015, 2, 1) }

(**
Endorphin.Instrument.Twickenham.MagnetController
======================

Documentation

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The Endorphin.Instrument.Twickenham.MagnetController library can be <a href="https://nuget.org/packages/Endorphin.Instrument.Twickenham.MagnetController">installed from NuGet</a>:
      <pre>PM> Install-Package Endorphin.Instrument.Twickenham.MagnetController -Pre</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>
*)

(** Example
-------

This shows how to connect to the magnet controller via F# with the library.

*)
let magnetController =
    MagnetController.openInstrument "GPIB0::4" 3000<ms> settings 
    |> Async.RunSynchronously

(** Samples & documentation
-----------------------

See how to:

 * [Ramp to a target field](fieldtarget.html)

 * [Perform a field sweep](fieldsweep.html)


The [API Reference](reference/index.html) contains automatically generated documentation for all types,
   modules and functions in the library.
 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read the [library design notes][readme] to understand how it works.

The library is available under Apache 2.0 license. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/fsprojects/Endorphin.Instrument.Twickenham.MagnetController/tree/master/docs/content
  [gh]: https://github.com/fsprojects/Endorphin.Instrument.Twickenham.MagnetController
  [issues]: https://github.com/fsprojects/Endorphin.Instrument.Twickenham.MagnetController/issues
  [readme]: https://github.com/fsprojects/Endorphin.Instrument.Twickenham.MagnetController/blob/master/README.md
  [license]: https://github.com/fsprojects/Endorphin.Instrument.Twickenham.MagnetController/blob/master/LICENSE.txt
*)
