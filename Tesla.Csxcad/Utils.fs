module Tesla.Csxcad.Utils

open System

let inline enumCode (e : 'TEnum) : string =
    let code = int e
    string code

let inline parseEnum (s : string) : 'TEnum =
    let value = int s
    let enumType = typeof<'TEnum>
    if not <| Enum.IsDefined (enumType, value) then
        let message = sprintf "Invalid value of %A: %d" enumType value
        raise <| ArgumentException (message, "s")

    enum value
