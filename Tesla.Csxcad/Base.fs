namespace Tesla.Csxcad.Base

open Tesla.Csxcad.Properties

type RectilinearGrid =
    { Delta : double
      XLines : double array
      YLines : double array
      ZLines : double array }

type ContinuousStructure =
    { Properties : Property array
      Grid : RectilinearGrid }
