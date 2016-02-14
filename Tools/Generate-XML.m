function copyWithoutHeader(sourceName, targetName, targetMode)
    input = fopen(sourceName);
    output = fopen(targetName, targetMode);
    line = fgetl(input);
    while (line != -1)
        line = fgetl(input);
        if (line != -1)
            fputs(output, strcat(line, "\n"));
        endif
    end
    fclose(input);
    fclose(output);
end

function concatXML (inputs, output)
    copyWithoutHeader(inputs{1}, output, 'w');
    cellfun(@(name) copyWithoutHeader(name, output, 'a'), inputs(2:end));
end

CSX = InitCSX();

mesh.x = -10:10;
mesh.y = -10:10;
mesh.z = -10:30;
CSX = DefineRectGrid(CSX, 1, mesh);

CSX = AddExcitation(CSX, 'ex-1', 0, [0 1 0]);
CSX = AddBox(CSX, 'ex-1', 0, [-10 -10 0], [10 10 0]);

CSX = AddExcitation(CSX, 'ex-2', 0, [0 1 0]);
CSX = AddBox(CSX, 'ex-2', 0, [-10 -10 7], [10 10 7]);

CSX = AddDump(CSX, 'Et');
CSX = AddBox(CSX, 'Et', 0, [-10 0 -10], [10 0 30]);
CSX = AddBox(CSX, 'Et', 0, [-5 5 -5], [5 5 5]);

CSX = AddMetal(CSX, 'Object 1');
CSX = AddMetal(CSX, 'Object 2');

CSX = AddBox(CSX, 'Object 1', 0, [0 0 0], [1 1 1]);
CSX = AddBox(CSX, 'Object 1', 0, [7 7 7], [8 8 8]);
CSX = AddBox(CSX, 'Object 2', 0, [3 3 3], [5 5 5]);

struct_2_xml('./CSX.xml', CSX, 'ContinuousStructure');

FDTD = InitFDTD();
FDTD = SetSinusExcite(FDTD, 10e6);
FDTD = SetBoundaryCond(FDTD, {'PMC' 'PMC' 'PEC' 'PEC' 'MUR' 'MUR'});

WriteOpenEMS('./openEMS.xml', FDTD, CSX);

concatXML({'./CSX.xml', './openEMS.xml', '../Test-Data/issue-16-test-data.csx'}, './Sample.lxml');
