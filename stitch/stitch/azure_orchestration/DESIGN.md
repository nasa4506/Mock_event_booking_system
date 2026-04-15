# Azure Orchestration: Design System

### 1. Overview & Creative North Star
**Creative North Star: The Orchestrated Flow**
Azure Orchestration is a high-end editorial design system built for the "Concierge" experience—an environment where precision meets luxury. It rejects the standard "SaaS-blue" grid in favor of a sophisticated, rhythmic layout that feels curated rather than generated. The system uses intentional white space, high-contrast typography, and depth-based layering to guide the user through a bespoke narrative journey.

### 2. Colors
The palette is rooted in deep architectural blues (`#00288e`) and sophisticated neutrals, creating an atmosphere of trust and exclusivity.

*   **The "No-Line" Rule:** Visual boundaries must be established through tonal shifts (e.g., `surface_container_lowest` against `surface`) rather than 1px borders. If a separator is required, use `outline_variant` at 10-20% opacity.
*   **Surface Hierarchy:** 
    *   **Background (`#f7f9fb`):** The canvas.
    *   **Lowest (`#ffffff`):** Foreground elements like cards and main navigation focus.
    *   **High/Highest (`#e6e8ea`):** Interactive utility areas like search bars and input fields.
*   **Signature Gradients:** The "Primary Gradient" (`#00288e` to `#1e40af` at 135°) is reserved for signature CTAs and hero Bento cards to create a focal point of energy.
*   **Glassmorphism:** The TopAppBar and floating overlays use `backdrop-blur-xl` with 80% opacity to maintain context while emphasizing the current layer.

### 3. Typography
Azure Orchestration uses **Manrope** for high-impact headlines and **Inter** for functional body text.

*   **Display (3.5rem):** Extra-bold, tracking-tighter. Used for editorial hero statements.
*   **Headline (1.5rem - 2.25rem):** Bold, used for section titles and primary headers.
*   **Body (0.875rem):** Standard reading size, set with relaxed leading for premium readability.
*   **Micro-Labels (10px - 11px):** Always uppercase with heavy tracking (0.2em). Used for categories and metadata to convey a "luxury watch" aesthetic.

### 4. Elevation & Depth
Elevation is achieved through "Tonal Layering" and sophisticated shadow work rather than stark borders.

*   **Shadow-SM:** Used for active nav items and small cards to provide a subtle "lift" from the page.
*   **Shadow-LG:** Used for primary action buttons and floating modals, creating a soft, diffused depth.
*   **The Layering Principle:** Use the `surface-container` tiers to stack information. For example, a search input (`surface-container-high`) sits within a header (`surface-container-lowest`), which sits on the main canvas (`surface`).
*   **Interactive Scaling:** Interactive elements like cards should use a subtle `scale-101` (1%) hover effect to provide tactile feedback without breaking the layout.

### 5. Components
*   **Buttons:** Primary buttons use the brand gradient with high-elevation shadows. Secondary actions use pill-shaped outlines at reduced opacity.
*   **Editorial Cards:** Must include a high-aspect-ratio image, generous padding (24px+), and a distinct "Top-Border" or "Bottom-Border" transition via `outline-variant/10`.
*   **Navigation:** Vertical sidebar uses high-density text (14px) and clear active states with background color shifts (`#ffffff`) and slight scaling.
*   **Bento Boxes:** Use the primary gradient for "Call to Action" cards within grids to break visual monotony.

### 6. Do's and Don'ts
*   **Do:** Use extreme typographic scale (pairing 10px labels with 3.5rem headlines).
*   **Do:** Use background color shifts to define content regions.
*   **Don't:** Use solid 1px black or high-contrast borders.
*   **Don't:** Overcrowd the grid; if in doubt, increase the `spacing` variable to level 3.
*   **Do:** Use high-quality, atmospheric photography that aligns with the "Concierge" aesthetic.