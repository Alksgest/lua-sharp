local filesWorker = require('filesWorker')

local arrayUp = 38;
local arrayDown = 40;

local menu = {}

menu.logicDrives = filesWorker.getLogicalDrives();
menu.currentSample = filesWorker.getLogicalDrives();
menu.currentPosisition = 0;

function menu.createMenu()
    local folders = filesWorker.getDirectories(menu.logicDrives[1])
    PrintLuaTable(menu.logicDrives);
end

function menu.start()
    while (true) do
        CleareConsole();
        PrintToConsole(menu.currentPosisition);
        PrintLuaTable(menu.currentSample, menu.currentPosisition);
        
        local result = HandleKeyPress();
        
        if (result == arrayUp) then
            if (menu.currentPosisition + 1 <= #menu.currentSample) then
                menu.currentPosisition = menu.currentPosisition + 1;
            else
                menu.currentPosisition = 1;
            end
        elseif (result == arrayDown) then
            if (menu.currentPosisition - 1 > 0) then
                menu.currentPosisition = menu.currentPosisition - 1;
            else
                menu.currentPosisition = #menu.currentSample;
            end
        end
    end
end

return menu;
