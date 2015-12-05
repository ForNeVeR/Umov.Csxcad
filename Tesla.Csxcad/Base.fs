module Tesla.Csxcad.Base

open Tesla.Csxcad.Properties

type RectilinearGrid = {
    Delta : double
    XLines : double seq
    YLines : double seq
    ZLines : double seq
}

type ContinuousStructure = {
    Properties : Property seq
    Grid : RectilinearGrid
}