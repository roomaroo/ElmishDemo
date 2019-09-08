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

let init _ = initialModel, []

let update msg model : Model * Cmd<Msg> = 
  match msg with
  | Increment -> { model with Count = model.Count + 1}, []
  | Decrement -> { model with Count = model.Count - 1}, []
  | Reset -> initialModel, []


let view model dispatch =
  let simpleButton txt action dispatch =
    div
      [ Style [Margin "5px"] ]
      [ a
          [ ClassName "button"
            OnClick (fun _ -> action |> dispatch) ]
          [ str txt ] ]

  div
    [ Style[ 
        Display "flex"; 
        FlexDirection "column"; 
        JustifyContent "center"; 
        AlignItems "center"
        Height "100%"] ]
    [ div
        []
        [ str (sprintf "Counter value: %i" model.Count) ]
      simpleButton "Increment" Increment dispatch
      simpleButton "Decrement" Decrement dispatch
      simpleButton "Reset" Reset dispatch ]


open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update view
|> Program.withDebugger
|> Program.withReact "elmish-app"
|> Program.run
