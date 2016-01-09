namespace Tesla.Csxcad.Properties

open Tesla.Csxcad.Geometry
open Tesla.Csxcad.Primitives

type ExcitationType =
    | SoftE = 0
    | HardE = 1
    | SoftH = 2
    | HardH = 3
    | PlaneWave = 10

type DumpType =
    | EFieldTimeDomain = 0
    | HFieldTimeDomain = 1
    | ElectricCurrentTimeDomain = 2
    | TotalCurrentDensityTimeDomain = 3
    | EFrequencyTimeDomain = 10
    | HFrequencyTimeDomain = 11
    | ElectricCurrentFrequencyDomain = 12
    | TotalCurrentDensityFrequencyDomain = 13
    | LocalSarFrequencyDomain = 20
    | AveragingSarFrequencyDomain1G = 21
    | AveragingSarFrequencyDomain10G = 22
    | RawDataForSarCalculations = 29

type DumpMode =
    | NoInterpolation = 0
    | NodeInterpolation = 1
    | CellInterpolation = 2

[<AbstractClass>]
type Property(name : string, zone : Primitive array) =
    member __.Name = name
    member __.Zone = zone
    override __.ToString () =
        sprintf "Property{ Name = %s, Zone = %A }" name zone

type Excitation(name : string,
                zone : Primitive array,
                ``type`` : ExcitationType,
                vector : Vector) =
    inherit Property(name, zone)
    member __.Type = ``type``
    member __.Vector = vector
    override __.ToString () =
        base.ToString () +
            sprintf ".Excitation{ Type = %A, Vector = %A }" ``type`` vector

type DumpBox(name : string,
             zone : Primitive array,
             dumpType : DumpType,
             dumpMode : DumpMode) =
    inherit Property(name, zone)
    member __.DumpType = dumpType
    member __.DumpMode = dumpMode
    override __.ToString () =
        base.ToString () +
            sprintf ".DumpBox{ DumpType = %A, DumpMode = %A }"
                    dumpType
                    dumpMode

type Metal(name : string,
           zone : Primitive array) =
    inherit Property(name, zone)
    override __.ToString () =
        base.ToString () + sprintf ".Metal{ }"
