namespace FabulousDemo

open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App = 
    type Model = { Count : int; StepSize : int; TimerOn : bool }

    type Msg = 
        | Increment 
        | Decrement 
        | Reset
        | SetStep of int
        | SetTimer of bool
        | TimerElapsed

    let initialModel = { Count = 0; StepSize = 1; TimerOn = false }

    let init () = initialModel, Cmd.none

    let timerCmd = 
        async {
            do! Async.Sleep 1000
            return TimerElapsed
        } |> Cmd.ofAsyncMsg
    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + model.StepSize }, Cmd.none
        | Decrement -> { model with Count = model.Count - model.StepSize }, Cmd.none
        | Reset -> initialModel, Cmd.none
        | SetStep n -> { model with StepSize = n}, Cmd.none
        | SetTimer on -> { model with TimerOn = on }, if on then timerCmd else Cmd.none
        | TimerElapsed ->
            {model with Count = model.Count + model.StepSize},
            if model.TimerOn then timerCmd else Cmd.none
                

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
            children = [ 
                View.Label(
                    text = sprintf "%d" model.Count, 
                    fontSize = 50,
                    textColor = (if model.Count < 0 then Color.Red else Color.Black),
                    horizontalTextAlignment=TextAlignment.Center)

                View.Slider(
                    minimumMaximum = (0.0, 10.0),
                    value = float model.StepSize,
                    horizontalOptions = LayoutOptions.FillAndExpand,
                    valueChanged = (fun args -> dispatch (SetStep (int args.NewValue)))
                )
                
                View.Label(
                    text = sprintf "Step: %d" model.StepSize,
                    fontSize = 30,
                    horizontalTextAlignment=TextAlignment.Center
                )

                View.Button(
                    text = "Increment", 
                    fontSize = 30,
                    command = (fun () -> dispatch Increment))

                View.Button(
                    text = "Decrement", 
                    fontSize = 30,
                    command = (fun () -> dispatch Decrement))

                View.Button(
                    text = "Reset", 
                    fontSize = 30,
                    command = (fun () -> dispatch Reset))

                View.StackLayout(
                    padding = 20.0,
                    scale = 2.0,
                    horizontalOptions = LayoutOptions.CenterAndExpand,
                    orientation = StackOrientation.Horizontal,
                    children = [
                        View.Label(text = "Timer:")
                        View.Switch(
                            isToggled = model.TimerOn,
                            toggled = (fun args -> dispatch (SetTimer args.Value)))
                        ])
            ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
        |> Program.withConsoleTrace
        |> XamarinFormsProgram.run app

    do runner.EnableLiveUpdate()
