module UmovCsxcad.Tests.UtilsTests

open System

open Xunit

open Umov.Csxcad
open Umov.Csxcad.Base
open Umov.Csxcad.Properties

[<Fact>]
let ``Utils.stringOption should behave properly`` () =
    Assert.Equal (None, Utils.stringOption "")
    Assert.Equal (None, Utils.stringOption null)
    Assert.Equal (Some "s", Utils.stringOption "s")

[<Fact>]
let ``Utils.enumCode should return enum value as string`` () =
    let enumMember = CoordinateSystem.Cartesian
    let code = Utils.enumCode enumMember
    Assert.Equal (code, "0")

[<Fact>]
let ``Utils.parseEnum should parse enums properly`` () =
    let enumMember = DumpType.EFrequencyTimeDomain
    let value = string <| int enumMember
    Assert.Equal (enumMember, Utils.parseEnum value)

[<Fact>]
let ``Utils.parseEnum should throw exception for unknown enum member`` () =
    let value = "777"
    let action = Action (fun () ->
                             let value : DumpType = Utils.parseEnum value
                             ())

    Assert.Throws<ArgumentException> action

let ``Utils.parseEnumOpt should return None for empty string`` () =
    let enum : CoordinateSystem option = Utils.parseEnumOpt ""
    Assert.Equal (None, enum)
