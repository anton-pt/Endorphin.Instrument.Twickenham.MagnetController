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

(**
Performing a field sweep
========================

This example demonstrates how to perform a field sweep with the magnet controller. *)

// specify the magnet controller settings
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

// specify the field sweep parameters
let sweepParameters =
    FieldSweep.Parameters.create Forward
    <| MagnetController.Settings.Convert.currentToStepIndex settings 0.5M<A>
    <| MagnetController.Settings.Convert.currentToStepIndex settings 0.9M<A>
    <| MagnetController.Settings.RampRate.nearestIndex settings 0.05M<A/s>


async {
    // connect to the magnet controller
    let! magnetController = MagnetController.openInstrument "GPIB0::4" 3000<ms> settings

    try
        // create a field sweep based on the parameters and print status updates
        let sweep = FieldSweep.create magnetController sweepParameters
        FieldSweep.status sweep |> Observable.add (printfn "%A")
    
        // prepare to sweep by ramping to the initial target and set ready to sweep
        let sweepHandle = FieldSweep.prepare sweep 
        FieldSweep.setReadyToSweep sweep

        // wait to finish and print the result
        let! result = FieldSweep.waitToFinish sweepHandle
        printfn "Magnetic field sweep finished with result: %A" result
    
    // afterwards, close the connection to the hardware
    finally MagnetController.closeInstrument magnetController |> Async.RunSynchronously }
|> Async.RunSynchronously