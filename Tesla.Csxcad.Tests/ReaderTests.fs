module Tesla.Csxcad.Tests.ReaderTests

open System
open System.IO

open Xunit

open Tesla.Csxcad
open Tesla.Csxcad.Tests.TestData

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
let ``OpenEmsReader should read openEMS file`` () =
    use stream = new FileStream(``openEMS.xml``, FileMode.Open, FileAccess.Read)
    ignore (OpenEmsReader.Parse stream)
    ()

[<Fact>]
let ``OpenEmsReader should throw an exception on invalid file`` () =
    use stream = new FileStream(``CSX.xml``, FileMode.Open, FileAccess.Read)
    Assert.Throws<Exception> (Func<obj> (fun () ->
                                             upcast OpenEmsReader.Parse stream))
