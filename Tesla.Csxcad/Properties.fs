module Tesla.Csxcad.Properties

open Tesla.Csxcad.Geometry
open Tesla.Csxcad.Primitives

type ExcitationType = 
      SoftE = 0
    | HardE = 1
    | SoftH = 2
    | HardH = 3
    | PlaneWave = 10

type DumpType =
      EFieldTimeDomain = 0
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
      NoInterpolation = 0
    | NodeInterpolation = 1
    | CellInterpolation = 2

type Property(name : string, zone : Primitive seq) =
    member __.Name = name
    member __.Zone = zone

type Excitation(name : string,
                zone : Primitive seq,
                ``type`` : ExcitationType,
                vector : Vector) =
    inherit Property(name, zone)
    member __.Type = ``type``
    member __.Vector = vector
    

type DumpBox(name : string,
             zone : Primitive seq,
             dumpType : DumpType,
             dumpMode : DumpMode) =
    inherit Property(name, zone)
    member __.DumpType = dumpType
    member __.DumpMode = dumpMode

type Metal(name : string,
           zone : Primitive seq) =
    inherit Property(name, zone)
