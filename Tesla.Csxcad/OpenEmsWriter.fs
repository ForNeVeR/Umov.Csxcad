module Tesla.Csxcad.OpenEmsWriter

open System.IO

open Tesla.Csxcad.Base

let makeExcitation (excitation : ExcitationMode) =
    // TODO: Write CutoffFrequency and ExcitationFunction also
    Xml.Excitation (name = None,
                    ``type`` = Utils.enumCode excitation.Type,
                    excite = None,
                    f0 = Option.map string excitation.MainFrequency,
                    primitives = None)

let makeBoundaryConditionType = function
    | Pec -> "PEC"
    | Pmc -> "PMC"
    | MurAbc -> "MUR"
    | PmlAbc size -> sprintf "PML_%d" size

let makeBoundaryCond (conditions : BoundaryConditions) =
    Xml.BoundaryCond (xmin = makeBoundaryConditionType conditions.XMin,
                      xmax = makeBoundaryConditionType conditions.XMax,
                      ymin = makeBoundaryConditionType conditions.YMin,
                      ymax = makeBoundaryConditionType conditions.YMax,
                      zmin = makeBoundaryConditionType conditions.ZMin,
                      zmax = makeBoundaryConditionType conditions.ZMax)


let private makeFdtd (fdtd : Fdtd) =
    Xml.Fdtd (numberOfTimesteps = string fdtd.NumberOfTimesteps,
              endCriteria = string fdtd.EndEnergyCriteria,
              fMax = string fdtd.MaxFrequency,
              excitation = makeExcitation fdtd.Excitation,
              boundaryCond = makeBoundaryCond fdtd.BoundaryConditions)

let private makeOpenEms (data : OpenEms) =
    let structure = CsxWriter.makeContinuousStructure data.ContinuousStructure
    Xml.OpenEms (fdtd = makeFdtd data.Fdtd,
                 continuousStructure = structure)

let Write (stream : Stream, data : OpenEms) : unit =
    let element = makeOpenEms data
    element.XElement.Save stream
