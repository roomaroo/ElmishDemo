namespace FabulousDemo

open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App = 
    
    type Model = 
      { Count : int }

    type Msg = 
        | Increment 
        | Decrement 

    
    let init () = { Count = 0 }, Cmd.none

    let update msg model =
        match msg with
        | Increment -> 
            { model with Count = model.Count + 1 }, Cmd.none
        | Decrement -> 
            { model with Count = model.Count - 1 }, Cmd.none

    
    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
            children = [ 
                View.Label(
                    text = sprintf "%d" model.Count, 
                    //horizontalOptions = LayoutOptions.Center, 
                    fontSize = 100,
                    widthRequest=200.0, 
                    horizontalTextAlignment=TextAlignment.Center)

                View.Button(
                    text = "Increment", 
                    fontSize = 30,
                    command = (fun () -> dispatch Increment))

                View.Button(
                    text = "Decrement", 
                    fontSize = 30,
                    command = (fun () -> dispatch Decrement))
            ]))

    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
        |> XamarinFormsProgram.run app

    do runner.EnableLiveUpdate()
