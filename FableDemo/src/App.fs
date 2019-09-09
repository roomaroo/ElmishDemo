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


open Zanaptak.TypedCssClasses

type Bulma = CssClasses<
              "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.css",
              Naming.PascalCase>

let view model dispatch =
  div
    [classList [(Bulma.Section, true); (Bulma.HasTextCentered, true)]]
    [
        h1 
          [ClassName 
            (match model.Count with
             | n when n < 0 -> Bulma.HasTextDanger
             | _ -> Bulma.HasTextInfo)]
          [str (sprintf "Counter value: %i" model.Count)]
        
        div 
          [ClassName Bulma.Buttons]
          [
            button 
              [ClassName Bulma.Button; OnClick (fun _ -> dispatch Decrement)] 
              [str "Decrement"]
            
            button 
              [ClassName Bulma.Button; OnClick (fun _ -> dispatch Increment)] 
              [str "Increment"]
            
            button 
              [
                classList [(Bulma.Button, true); (Bulma.IsPrimary, true)];
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
