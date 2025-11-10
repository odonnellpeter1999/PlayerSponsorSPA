## Quick context for AI code assistants

This is a small React + TypeScript app scaffolded to run with Vite. Key technologies:
- React 19 (functional components + hooks)
- TypeScript 5.x with project references (see `tsconfig.*.json`)
- Vite as the dev server / bundler
- MUI v7 (`@mui/material`, `@emotion/*`) used for UI components
- @tanstack/react-router for routing (see `src/routes`)

Primary goals for assistant edits
- Make small, well-scoped changes: prefer editing route components under `src/routes`, shared UI in `src/` (e.g. `App.tsx`, `main.tsx`).
- Preserve TypeScript types and React patterns. Run the build/typecheck commands after edits.

Important files to reference
- `package.json` — scripts you can run: `npm run dev`, `npm run build`, `npm run lint`, `npm run preview`.
- `tsconfig.app.json`, `tsconfig.json` — TypeScript project layout and config.
- `vite.config.ts` — Vite plugins and dev server settings.
- `src/routes` — route components (routing is handled by @tanstack/react-router).
- `.vscode/mcp.json` — shows an MCP server entry for `@mui/mcp` (used by the editor). Avoid removing this.

Build / test / debug workflows
- Start development server with HMR: `npm run dev` (uses Vite). Use this when making UI or routing changes.
- Full build + type-check: `npm run build` (runs `tsc -b` then `vite build`). Use after adding/changing TypeScript types or public API.
- Quick lint pass: `npm run lint`.

Project-specific conventions & patterns
- Routing: Uses `@tanstack/react-router`. Look at `src/routeTree.gen.ts` and `src/routes/*.tsx` for the route definitions and layout. When adding pages, add a file in `src/routes` and wire it into the route tree.
- Styling: Uses MUI + Emotion. Prefer MUI primitives and `sx` prop or `styled` from `@emotion/styled` instead of global CSS when adding components. Global styles live in `src/index.css` and `src/App.css`.
- Type checking: This repo runs `tsc -b` as part of `npm run build`. Small edits that change public types may require running `tsc -b` to verify.

Integration points & external dependencies
- MUI: `@mui/material`, `@emotion/*`. There is also an MCP server entry for `@mui/mcp` in `.vscode/mcp.json`. This is an editor tool; do not modify it unless updating editor config.
- Router: `@tanstack/react-router` and `@tanstack/react-router-devtools` are present. Modifying routes may require adjusting generated route tree (`routeTree.gen.ts`).

Examples of common edits
- Add a new page: create `src/routes/newPage.tsx` exporting a default component, then import/register it in the route tree or parent route. Use the existing `index.tsx` and `__root.tsx` as examples.
- Add a typed prop to a component: update the component's `Props` interface, update usages across `src/` and run `npm run build` to ensure type safety.

What not to change
- Do not change project-wide TypeScript settings (`tsconfig*.json`) unless explicitly asked — these affect builds across the repo.
- Avoid modifying `vite.config.ts` or `package.json` scripts without clear reason.

If you need clarification
- Ask for the intent: UI change, bugfix, or architecture change. For anything touching build config, request permission before proceeding.

Quick checklist for PRs created by an AI assistant
- Ensure `npm run build` succeeds locally (or state the exact failures).
- Keep changes isolated to a few files and update `routeTree.gen.ts` only when adding/removing routes.
- Add short tests or type-only checks where appropriate (optional).

Feedback and iteration
If parts of the codebase are unclear (route wiring, generated files, editor MCP usage), tell me which files you want clarified and I will expand this guidance.
