function Register-EditorExtension {
	[CmdletBinding()]
	param(
        [Parameter(
            Mandatory=$true,
            ParameterSetName="CommandFunction")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="CommandScriptBlock")]
		[switch]$Command,

        [Parameter(
            Mandatory=$true,
            ParameterSetName="AnalyzerFunction")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="AnalyzerScriptBlock")]
		[switch]$Analyzer,

        [Parameter(
            Mandatory=$true,
            ParameterSetName="FormatterFunction")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="FormatterScriptBlock")]
		[switch]$Formatter,

        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$Name,

        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$DisplayName,

        [Parameter(
            Mandatory=$true,
            ParameterSetName="CommandFunction")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="AnalyzerFunction")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="FormatterFunction")]
        [ValidateNotNullOrEmpty()]
        [string]$Function,

        [Parameter(
            Mandatory=$true,
            ParameterSetName="CommandScriptBlock")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="AnalyzerScriptBlock")]
        [Parameter(
            Mandatory=$true,
            ParameterSetName="FormatterScriptBlock")]
        [ValidateNotNullOrEmpty()]
        [ScriptBlock]$ScriptBlock

        ### Command Extension Parameters

        ### Analyzer Extension Parameters

        ### Formatter Extension Parameters
	)

    Process
    {
        $hasScriptBlock = $ScriptBlock -ne $null

        if ($Command.IsPresent)
        {
            if ($hasScriptBlock)
            {
                Write-Verbose "Registering Command with ScriptBlock"
				$psEditor.RegisterCommand($Name, $DisplayName, $ScriptBlock, $null);
            }
            else
            {
                Write-Verbose "Registering Command with function"
				$psEditor.RegisterCommand($Name, $DisplayName, "$Function", $null);
            }
        }
        elseif($Analyzer.IsPresent)
        {
            if ($hasScriptBlock)
            {
                Write-Verbose "Registering Analyzer with ScriptBlock"
            }
            else
            {
                Write-Verbose "Registering Analyzer with function"
            }
        }
        elseif($Formatter.IsPresent)
        {
            if ($hasScriptBlock)
            {
                Write-Verbose "Registering Formatter with ScriptBlock"
            }
            else
            {
                Write-Verbose "Registering Formatter with function"
            }
        }
    }
}

function Unregister-EditorExtension {
	[CmdletBinding()]
	param(
        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$Name
	)

    Process
    {
        Write-Verbose "Unregistering extension named '$Name'"
    }
}

#$file = $psEditor.GetFile("file path"); # include param to create if it doesn't exist
## $file is same was $commandContext.CurrentFile

#$commandContext.CurrentFile.InsertText("boo", $range);
#$commandContext.CurrentFile.Contents;
#$commandContext.CurrentFile.Ast;
#$commandContext.CurrentFile.Extension;
#$commandContext.SelectedRange;
#$commandContext.CursorPosition;
#$commandContext.SetSelection($range);

#Register-EditorExtension -Verbose `
#    -Command `
#    -Name "MyCommand" `
#    -DisplayName "My command" `
#    -ScriptBlock { Write-Output "My command script block!" }

#function Invoke-MyCommand { Write-Output "My command function!" }

#Register-EditorExtension -Verbose `
#    -Command `
#    -Name "MyCommand" `
#    -DisplayName "My command" `
#    -Function Invoke-MyCommand


#Register-EditorExtension -Verbose `
#    -Analyzer `
#    -Name "MyAnalyzerScriptBlock" `
#    -DisplayName "My analyzer" `
#    -ScriptBlock { Write-Output "My analyzer script block!" }

#function Invoke-MyAnalyzer { Write-Output "My analyzer function!" }

#Register-EditorExtension -Verbose `
#    -Analyzer `
#    -Name "MyAnalyzerFunction" `
#    -DisplayName "My analyzer" `
#    -Function Invoke-MyAnalyzer


#Register-EditorExtension -Verbose `
#    -Formatter `
#    -Name "MyFormatter" `
#    -DisplayName "My formatter" `
#    -ScriptBlock { Write-Output "My formatter script block!" }

#function Invoke-MyFormatter { Write-Output "My formatter function!" }

#Register-EditorExtension -Verbose `
#    -Formatter `
#    -Name "MyFormatterFunction" `
#    -DisplayName "My formatter function" `
#    -Function Invoke-MyFormatter

#Unregister-EditorExtension -Name "MyCommand" -Verbose