module Tesla.Csxcad.Tests.UtilsTests

open System

open Xunit

open Tesla.Csxcad
open Tesla.Csxcad.Base
open Tesla.Csxcad.Properties

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
