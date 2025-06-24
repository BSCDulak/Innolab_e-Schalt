# How many rows and columns in the logical grid?
GRID_ROWS = 4
GRID_COLS = 3

# For each component type, define allowed cells
ZONE_RULES = {
    "relay": [(0, 0), (0, 1), (1, 0), (1, 1)],    # Top/mid left
    "switch": [(2, 1), (3, 1)],                   # Mid/bottom middle
    "power": [(3, 0), (3, 1), (3, 2)],            # Bottom area
    # could add more
    # add more hehe
}
