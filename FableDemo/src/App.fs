module App.View

open Elmish
open Fable.Helpers.React
open Fable.Helpers.React.Props

type Model = { Count: int }

type Msg = 
  | Increment
  | Decrement
  | Reset

let initialModel = { Count = 0 }

let init _ = initialModel, Cmd.none

let update msg model : Model * Cmd<Msg> = 
  match msg with
  | Increment -> { model with Count = model.Count + 1}, Cmd.none
  | Decrement -> { model with Count = model.Count - 1}, Cmd.none
  | Reset -> initialModel, Cmd.none

let view model dispatch =
  div
    [ClassName "section has-text-centered"]
    [
        h1 
          []
          [str (sprintf "Counter value: %i" model.Count)]
        
        div 
          []
          [
            button 
              [ClassName "button"; OnClick (fun _ -> dispatch Decrement)] 
              [str "Decrement"]
            
            button 
              [ClassName "button"; OnClick (fun _ -> dispatch Increment)] 
              [str "Increment"]
            
            button 
              [
                classList [("button", true); ("is-danger", true)];
                OnClick (fun _ -> dispatch Reset)] 
              [str "Reset"]
          ]
    ]

open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update view
|> Program.withDebugger
|> Program.withReact "elmish-app"
|> Program.run
