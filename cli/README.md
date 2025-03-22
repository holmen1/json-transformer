# CLI

A command-line tool for transforming JSON assumptions data into a nested dictionary structure.

## Overview

This CLI tool processes input JSON files containing assumption data and converts them into a 3-level nested dictionary structure, saving the result as a new JSON file.

## Usage

```bash
dotnet run -- <input-file-path> [output-file-path]
```

If no output path is specified, the result will be saved to `output.json` in the current directory.

## Input Format

The input JSON should follow this structure:

```json
{
  "Assumptions": [
    {
      "level1": "A",
      "level2": "HR",
      "level3": "Investment",
      "value": 0.0
    },
    ...
  ]
}
```

## Output Format

```json
{
  "A": {
    "HR": {
      "Investment": 0.0,
      "Cash": 0.0
    },
    "LR": {
      "Investment": 0.0,
      "Cash": 0.0
    }
  },
  "B": {
    "HR": {
      "Investment": 0.0,
      "Cash": 6.66
    },
    ...
  }
}
```

## Troubleshooting

- Property names in the JSON are case-insensitive
- Level values must match the defined enum types in the domain model