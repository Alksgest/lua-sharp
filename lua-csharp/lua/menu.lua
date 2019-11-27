local filesWorker = require('filesWorker')

local consoleKey = {
    backspace = 8,
    enterKey = 13,
    arrayUp = 38,
    arrayDown = 40,
}

local function copy(obj)
    if type(obj) ~= 'table' then return obj end
    local res = {}
    for k, v in pairs(obj) do res[copy(k)] = copy(v) end
    return res
end

local function mergeArrays(arr1, arr2) --  first folders, second files
    local resArray = {};
    
    for i = 1, #arr1 do
        resArray[#resArray + 1] = arr1[i];
    end
    for j = 1, #arr2 do
        resArray[#resArray + 1] = arr2[j];
    end
    
    return resArray;
end

local menu = {}

menu.logicDrives = filesWorker.getLogicalDrives();
menu.currentSample = filesWorker.getLogicalDrives();
menu.currentPath = '';
menu.currentPosisition = 0;

function menu.createMenu()
    local folders = filesWorker.getDirectories(menu.logicDrives[1])
    PrintLuaTable(menu.logicDrives);
end

function menu.handleKeyUp(self)
    if (self.currentPosisition - 1 >= 1) then
        self.currentPosisition = self.currentPosisition - 1;
    else
        self.currentPosisition = #self.currentSample;
    end
end

function menu.handleKeyDown(self)
    if (self.currentPosisition + 1 <= #self.currentSample) then
        self.currentPosisition = self.currentPosisition + 1;
    else
        self.currentPosisition = 1;
    end
end

function menu.handleKeyEnter(self)
    if (CheckIsPathPresentFile(self.currentSample[self.currentPosisition])) then
        local file = self.currentSample[self.currentPosisition];
        RunFile(file);
    else
        self.currentPath = self.currentSample[self.currentPosisition];
        
        local folders = filesWorker.getDirectories(self.currentPath);
        local filesInFolder = filesWorker.getFiles(self.currentPath);
        
        self.currentSample = mergeArrays(folders, filesInFolder);
        
        self.currentPosisition = 0;
    end
end

function menu.handleKeyBackspace(self)
    if (self.currentPath ~= '' and #self.currentPath > 4) then
        local parentFolder = GetParentFolder(self.currentPath);

        local folders = filesWorker.getDirectories(parentFolder);
        local filesInFolder = filesWorker.getFiles(parentFolder);
        
        self.currentSample = mergeArrays(folders, filesInFolder);
        self.currentPath = parentFolder;
        self.currentPosisition = 0;
    else
        self.currentSample = filesWorker.getLogicalDrives();
        self.currentPosisition = 0;
    end
end

function menu.start(self)
    while (true) do
        CleareConsole();
        PrintToConsole(self.currentPosisition);
        PrintLuaTable(self.currentSample, self.currentPosisition);
        
        local result = HandleKeyPress();
        
        if (result == consoleKey.arrayUp) then
            self:handleKeyUp();
        elseif (result == consoleKey.arrayDown) then
            self:handleKeyDown();
        elseif (result == consoleKey.enterKey) then
            self:handleKeyEnter();
        elseif (result == consoleKey.backspace) then
            self:handleKeyBackspace();
        end
    end
end

return menu;
