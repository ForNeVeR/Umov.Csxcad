module Umov.Csxcad.Tests.ReaderTests

open System
open System.IO

open Xunit

open Umov.Csxcad
open Umov.Csxcad.Properties
open Umov.Csxcad.Tests.TestData

[<Fact>]
let ``CsxReader should read CSX file`` () =
    use stream = new FileStream(``CSX.xml``, FileMode.Open, FileAccess.Read)
    ignore (CsxReader.Parse stream)
    ()

[<Fact>]
let ``CsxReader should throw an exception on invalid file`` () =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open, FileAccess.Read)
    Assert.Throws<Exception> (Func<obj> (fun () ->
                                             upcast CsxReader.Parse stream))

[<Fact>]
let ``CsxReader should read issue-16-test-data.cxs successfully`` () =
    use stream = new FileStream(``Test-Data/issue-16-test-data.csx``, FileMode.Open, FileAccess.Read)
    ignore (CsxReader.Parse stream)

[<Fact>]
let ``CsxReader should read material list successfully`` () =
    use stream = new FileStream(``Test-Data/issue-16-test-data.csx``, FileMode.Open, FileAccess.Read)
    let structure = CsxReader.Parse stream
    let materialCount =
        structure.Properties
        |> Utils.ofType<Material>
        |> Seq.length

    Assert.Equal (4, materialCount)

[<Fact>]
let ``OpenEmsReader should read openEMS file`` () =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open, FileAccess.Read)
    ignore (OpenEmsReader.Parse stream)

[<Fact>]
let ``OpenEmsReader should throw an exception on invalid file`` () =
    use stream = new FileStream(``CSX.xml``, FileMode.Open, FileAccess.Read)
    Assert.Throws<Exception> (Func<obj> (fun () ->
                                             upcast OpenEmsReader.Parse stream))

[<Fact>]
let ``OpenEmsReader should read Test-Data/CSX.xml successfully`` () =
    use stream = new FileStream(``Test-Data/CSX.xml``, FileMode.Open, FileAccess.Read)
    ignore (CsxReader.Parse stream)
