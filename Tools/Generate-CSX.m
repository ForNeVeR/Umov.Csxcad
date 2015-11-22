CSX = InitCSX();

mesh.x = -10:10;
mesh.y = -10:10;
mesh.z = -10:30;
CSX = DefineRectGrid(CSX, 1, mesh);

CSX = AddExcitation(CSX, 'excitation', 0, [0 1 0]);
CSX = AddBox(CSX, 'excitation', 0, [-10 -10 0], [10 10 0]);

CSX = AddDump(CSX, 'Et');
CSX = AddBox(CSX, 'Et', 0, [-10 0 -10], [10 0 30]);

struct_2_xml('./CSX.xml', CSX, 'ContinuousStructure');
