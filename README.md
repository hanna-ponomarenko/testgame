# Match-3 MVP Plan

`README.md` was missing in this repository, while `Testgame/README.rtf` contains the full specification.
This file is a concise MVP extraction from that spec and is treated as the implementation source of truth.

## Clarification options (chosen simplest)
When the full spec is broad, there are multiple implementation options:
1. Implement every advanced shape/special piece rule now.
2. Implement core board loop plus straight-line matching and cascades only.
3. Implement only a static parser with no gameplay loop.

Chosen: **Option 2 (simplest playable MVP)**.

## MVP scope
- Portrait match-3 core logic.
- Parse level from ASCII text format:
  - First line: `<width> <height>`
  - Next `height` lines: `width` integers.
- Board cells use piece IDs `1..10`.
- Detect and remove matches of 3+ in horizontal/vertical lines.
- Apply gravity (pieces fall down).
- Refill empty cells with random IDs `1..10`.
- Resolve cascades until no matches remain.

## Explicitly out of MVP
- Bomb/Arrow/Ball creation and activation.
- L/T/Cross shape handling.
- Swap input and rendering/animation.

## Priority
If a feature appears both in this file and in broad RTF text, this MVP file wins for this repository iteration.
