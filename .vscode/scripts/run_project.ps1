param(
  [Parameter(Mandatory=$true)]
  [string]$startDir,
  [switch]$run
)

function Find-Csproj {
  param([string]$dir)
  while ($dir) {
    # ignore obj and bin directories
    $base = Split-Path $dir -Leaf
    if ($base -in @('obj','bin')) {
      $dir = Split-Path $dir -Parent
      continue
    }
    try {
      $cs = Get-ChildItem -Path $dir -Filter *.csproj -File -ErrorAction SilentlyContinue | Select-Object -First 1
    } catch {
      $cs = $null
    }
    if ($cs) { return $cs.FullName }
    $parent = Split-Path $dir -Parent
    if ([string]::IsNullOrEmpty($parent) -or $parent -eq $dir) { break }
    $dir = $parent
  }
  return $null
}

$startDir = (Resolve-Path -Path $startDir).ProviderPath
$proj = Find-Csproj $startDir
if (-not $proj) {
  Write-Error "No se encontró ningún .csproj empezando en $startDir y subiendo directorios."
  exit 1
}
Write-Output "Proyecto encontrado: $proj"

$build = dotnet build "$proj"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

if ($run) {
  $projDir = Split-Path $proj -Parent
  $projName = [System.IO.Path]::GetFileNameWithoutExtension($proj)
  $dllPath = Join-Path $projDir "bin\Debug\net9.0\$projName.dll"
  if (-not (Test-Path $dllPath)) {
    $found = Get-ChildItem -Path (Join-Path $projDir 'bin\Debug') -Recurse -Filter "$projName.dll" -File -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($found) { $dllPath = $found.FullName } else {
      # fallback: any dll in bin/Debug/net*/
      $found = Get-ChildItem -Path (Join-Path $projDir 'bin\Debug') -Recurse -Filter '*.dll' -File -ErrorAction SilentlyContinue | Select-Object -First 1
      if ($found) { $dllPath = $found.FullName } else {
        Write-Error "No se encontró el DLL de salida para ejecutar en $projDir\bin\Debug."
        exit 2
      }
    }
  }
  Write-Output "Ejecutando: $dllPath"
  dotnet "$dllPath"
}
