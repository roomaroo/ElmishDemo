namespace FabulousDemo

open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App = 
    type Model = { Count : int }

    type Msg = 
        | Increment 
        | Decrement 
        | Reset


    let initialModel = { Count = 0 }
    let init () = initialModel, Cmd.none

    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }, Cmd.none
        | Decrement -> { model with Count = model.Count - 1 }, Cmd.none
        | Reset -> initialModel, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
            children = [ 
                View.Label(
                    text = sprintf "%d" model.Count, 
                    fontSize = 50,
                    textColor = (if model.Count < 0 then Color.Red else Color.Black),
                    horizontalTextAlignment=TextAlignment.Center)

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
