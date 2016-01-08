module Tesla.Csxcad.Tests.ReaderTests

open System
open System.IO

open Xunit

open Tesla.Csxcad

let Tools = Path.Combine ("..", "..", "..", "Tools")
let ``CSX.xml`` = Path.Combine (Tools, "CSX.xml")
let ``openEMS.xml`` = Path.Combine (Tools, "openEMS.xml")


[<Fact>]
let ``CsxReader should read CSX file`` () =
    use stream = new FileStream(``CSX.xml``, FileMode.Open)
    ignore (CsxReader.Parse stream)
    ()

[<Fact>]
let ``CsxReader should throw an exception on invalid file`` () =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open)
    Assert.Throws<Exception> (Func<obj> (fun () ->
                                             upcast CsxReader.Parse stream))

[<Fact>]
let ``OpenEmsReader should read openEMS file`` () =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open)
    ignore (OpenEmsReader.Parse stream)
    ()

[<Fact>]
let ``OpenEmsReader should throw an exception on invalid file`` () =
    use stream = new FileStream(``CSX.xml``, FileMode.Open)
    Assert.Throws<Exception> (Func<obj> (fun () ->
                                             upcast OpenEmsReader.Parse stream))
