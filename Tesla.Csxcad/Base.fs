namespace Tesla.Csxcad.Base

open Tesla.Csxcad.Properties

type CoordinateSystem =
    | Cartesian = 0
    | Cylindrical = 1
    /// Not yet implemented per comment in InitCSX.m.
    | Spherical = 2

type ExcitationModeType =
    | Gauss = 0
    | Sine = 1
    | Dirac = 2
    | Step = 3
    | Custom = 10

type BoundaryConditionType =
    /// Perfect electrical conductor.
    | Pec
    /// Perfect magnetic conductor.
    | Pmc
    /// Mur absorbing boundary conditions.
    | MurAbc
    /// Perfectly matched layers absorbing boundary conditions.
    | PmlAbc of PmlSize : uint32

type RectilinearGrid =
    { Delta : double
      CoordinateSystem : CoordinateSystem
      XLines : double array
      YLines : double array
      ZLines : double array }

type ContinuousStructure =
    { CoordinateSystem : CoordinateSystem
      Properties : Property array
      Grid : RectilinearGrid }

type ExcitationMode =
    { Type : ExcitationModeType

      /// Nyquist rate for custom excitation mode, center frequency for Gauss
      /// excitation, frequency for sine excitation. Not defined otherwise.
      MainFrequency : double option

      /// For Gauss excitation only. 20 dB cutoff frequency.
      CutoffFrequency : double option

      /// Custom excitation function definition.
      ExcitationFunction : string option }

type BoundaryConditions =
    { XMin : BoundaryConditionType
      XMax : BoundaryConditionType
      YMin : BoundaryConditionType
      YMax : BoundaryConditionType
      ZMin : BoundaryConditionType
      ZMax : BoundaryConditionType }

type Fdtd =
    { NumberOfTimesteps : uint32
      EndEnergyCriteria : double
      MaxFrequency : double
      Excitation : ExcitationMode
      BoundaryConditions : BoundaryConditions }

type OpenEms =
    { Fdtd : Fdtd
      ContinuousStructure : ContinuousStructure }
