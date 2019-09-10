module App.View

open Elmish
open Elmish.Browser.Navigation
open Fable.Helpers.React
open Fable.Helpers.React.Props

type Model = { Count: int }

type Msg = 
  | Increment
  | Decrement
  | Reset

let initialModel = { Count = 0 }

let init _ = initialModel

let update msg model : Model = 
  match msg with
  | Increment -> { model with Count = model.Count + 1}
  | Decrement -> { model with Count = model.Count - 1}
  | Reset -> initialModel


let view model dispatch =
    
  div
    []
    [ h1
        []
        [ str (sprintf "Counter value: %i" model.Count) ]
      button 
        [OnClick (fun _ -> dispatch Increment)]
        [str "Increment"]
      button 
        [OnClick (fun _ -> dispatch Decrement)]
        [str "Decrement"]
      button 
        [OnClick (fun _ -> dispatch Reset)]
        [str "Reset"]        
    ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkSimple init update view
|> Program.withDebugger
|> Program.withReact "elmish-app"
|> Program.run
