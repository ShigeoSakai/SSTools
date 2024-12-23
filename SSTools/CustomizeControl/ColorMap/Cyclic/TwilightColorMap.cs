﻿using System.Drawing;
namespace SSTools.ColorMap.Cyclic
{
	/// <summary>
	/// Twilightカラーマップ
	/// </summary>
	public class TwilightColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TwilightColorMap()
		{
			colorMap = twilight_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] twilight_map_ =
		{
			Color.FromArgb(225,216,226),		// 0
			Color.FromArgb(225,216,226),		// 1
			Color.FromArgb(224,217,226),		// 2
			Color.FromArgb(224,217,225),		// 3
			Color.FromArgb(223,217,225),		// 4
			Color.FromArgb(223,217,225),		// 5
			Color.FromArgb(222,217,224),		// 6
			Color.FromArgb(221,217,224),		// 7
			Color.FromArgb(221,217,224),		// 8
			Color.FromArgb(220,216,223),		// 9
			Color.FromArgb(219,216,223),		// 10
			Color.FromArgb(218,216,222),		// 11
			Color.FromArgb(217,216,222),		// 12
			Color.FromArgb(217,215,222),		// 13
			Color.FromArgb(216,215,221),		// 14
			Color.FromArgb(215,215,221),		// 15
			Color.FromArgb(214,214,220),		// 16
			Color.FromArgb(213,214,220),		// 17
			Color.FromArgb(212,214,219),		// 18
			Color.FromArgb(211,213,219),		// 19
			Color.FromArgb(210,213,218),		// 20
			Color.FromArgb(208,212,218),		// 21
			Color.FromArgb(207,212,217),		// 22
			Color.FromArgb(206,211,216),		// 23
			Color.FromArgb(205,210,216),		// 24
			Color.FromArgb(203,210,215),		// 25
			Color.FromArgb(202,209,215),		// 26
			Color.FromArgb(201,209,214),		// 27
			Color.FromArgb(199,208,214),		// 28
			Color.FromArgb(198,207,213),		// 29
			Color.FromArgb(197,207,212),		// 30
			Color.FromArgb(195,206,212),		// 31
			Color.FromArgb(194,205,211),		// 32
			Color.FromArgb(192,205,211),		// 33
			Color.FromArgb(191,204,210),		// 34
			Color.FromArgb(189,203,210),		// 35
			Color.FromArgb(188,202,209),		// 36
			Color.FromArgb(186,202,208),		// 37
			Color.FromArgb(185,201,208),		// 38
			Color.FromArgb(183,200,207),		// 39
			Color.FromArgb(182,199,207),		// 40
			Color.FromArgb(180,199,206),		// 41
			Color.FromArgb(179,198,206),		// 42
			Color.FromArgb(177,197,205),		// 43
			Color.FromArgb(176,196,205),		// 44
			Color.FromArgb(174,196,204),		// 45
			Color.FromArgb(173,195,204),		// 46
			Color.FromArgb(171,194,204),		// 47
			Color.FromArgb(170,193,203),		// 48
			Color.FromArgb(168,192,203),		// 49
			Color.FromArgb(167,192,202),		// 50
			Color.FromArgb(165,191,202),		// 51
			Color.FromArgb(164,190,202),		// 52
			Color.FromArgb(162,189,201),		// 53
			Color.FromArgb(161,188,201),		// 54
			Color.FromArgb(159,187,201),		// 55
			Color.FromArgb(158,187,200),		// 56
			Color.FromArgb(156,186,200),		// 57
			Color.FromArgb(155,185,200),		// 58
			Color.FromArgb(154,184,199),		// 59
			Color.FromArgb(152,183,199),		// 60
			Color.FromArgb(151,182,199),		// 61
			Color.FromArgb(150,181,198),		// 62
			Color.FromArgb(148,180,198),		// 63
			Color.FromArgb(147,180,198),		// 64
			Color.FromArgb(146,179,198),		// 65
			Color.FromArgb(144,178,197),		// 66
			Color.FromArgb(143,177,197),		// 67
			Color.FromArgb(142,176,197),		// 68
			Color.FromArgb(140,175,197),		// 69
			Color.FromArgb(139,174,197),		// 70
			Color.FromArgb(138,173,196),		// 71
			Color.FromArgb(137,172,196),		// 72
			Color.FromArgb(136,171,196),		// 73
			Color.FromArgb(134,171,196),		// 74
			Color.FromArgb(133,170,196),		// 75
			Color.FromArgb(132,169,195),		// 76
			Color.FromArgb(131,168,195),		// 77
			Color.FromArgb(130,167,195),		// 78
			Color.FromArgb(129,166,195),		// 79
			Color.FromArgb(128,165,195),		// 80
			Color.FromArgb(127,164,194),		// 81
			Color.FromArgb(126,163,194),		// 82
			Color.FromArgb(125,162,194),		// 83
			Color.FromArgb(124,161,194),		// 84
			Color.FromArgb(123,160,194),		// 85
			Color.FromArgb(122,159,194),		// 86
			Color.FromArgb(121,158,193),		// 87
			Color.FromArgb(120,157,193),		// 88
			Color.FromArgb(119,156,193),		// 89
			Color.FromArgb(118,155,193),		// 90
			Color.FromArgb(117,154,193),		// 91
			Color.FromArgb(116,154,193),		// 92
			Color.FromArgb(115,153,193),		// 93
			Color.FromArgb(115,152,192),		// 94
			Color.FromArgb(114,151,192),		// 95
			Color.FromArgb(113,150,192),		// 96
			Color.FromArgb(112,149,192),		// 97
			Color.FromArgb(111,148,192),		// 98
			Color.FromArgb(111,147,192),		// 99
			Color.FromArgb(110,146,191),		// 100
			Color.FromArgb(109,145,191),		// 101
			Color.FromArgb(109,144,191),		// 102
			Color.FromArgb(108,143,191),		// 103
			Color.FromArgb(107,142,191),		// 104
			Color.FromArgb(107,141,191),		// 105
			Color.FromArgb(106,139,190),		// 106
			Color.FromArgb(106,138,190),		// 107
			Color.FromArgb(105,137,190),		// 108
			Color.FromArgb(104,136,190),		// 109
			Color.FromArgb(104,135,190),		// 110
			Color.FromArgb(103,134,190),		// 111
			Color.FromArgb(103,133,189),		// 112
			Color.FromArgb(102,132,189),		// 113
			Color.FromArgb(102,131,189),		// 114
			Color.FromArgb(101,130,189),		// 115
			Color.FromArgb(101,129,189),		// 116
			Color.FromArgb(101,128,188),		// 117
			Color.FromArgb(100,127,188),		// 118
			Color.FromArgb(100,126,188),		// 119
			Color.FromArgb(100,125,188),		// 120
			Color.FromArgb(99,124,187),		// 121
			Color.FromArgb(99,123,187),		// 122
			Color.FromArgb(99,122,187),		// 123
			Color.FromArgb(98,120,187),		// 124
			Color.FromArgb(98,119,186),		// 125
			Color.FromArgb(98,118,186),		// 126
			Color.FromArgb(97,117,186),		// 127
			Color.FromArgb(97,116,186),		// 128
			Color.FromArgb(97,115,185),		// 129
			Color.FromArgb(97,114,185),		// 130
			Color.FromArgb(97,113,185),		// 131
			Color.FromArgb(96,112,184),		// 132
			Color.FromArgb(96,110,184),		// 133
			Color.FromArgb(96,109,184),		// 134
			Color.FromArgb(96,108,183),		// 135
			Color.FromArgb(96,107,183),		// 136
			Color.FromArgb(96,106,183),		// 137
			Color.FromArgb(95,105,182),		// 138
			Color.FromArgb(95,104,182),		// 139
			Color.FromArgb(95,103,182),		// 140
			Color.FromArgb(95,101,181),		// 141
			Color.FromArgb(95,100,181),		// 142
			Color.FromArgb(95,99,180),		// 143
			Color.FromArgb(95,98,180),		// 144
			Color.FromArgb(95,97,180),		// 145
			Color.FromArgb(95,96,179),		// 146
			Color.FromArgb(95,94,179),		// 147
			Color.FromArgb(94,93,178),		// 148
			Color.FromArgb(94,92,178),		// 149
			Color.FromArgb(94,91,177),		// 150
			Color.FromArgb(94,90,177),		// 151
			Color.FromArgb(94,89,176),		// 152
			Color.FromArgb(94,87,176),		// 153
			Color.FromArgb(94,86,175),		// 154
			Color.FromArgb(94,85,175),		// 155
			Color.FromArgb(94,84,174),		// 156
			Color.FromArgb(94,83,173),		// 157
			Color.FromArgb(94,81,173),		// 158
			Color.FromArgb(94,80,172),		// 159
			Color.FromArgb(94,79,172),		// 160
			Color.FromArgb(94,78,171),		// 161
			Color.FromArgb(94,77,170),		// 162
			Color.FromArgb(94,75,170),		// 163
			Color.FromArgb(94,74,169),		// 164
			Color.FromArgb(93,73,168),		// 165
			Color.FromArgb(93,72,167),		// 166
			Color.FromArgb(93,70,167),		// 167
			Color.FromArgb(93,69,166),		// 168
			Color.FromArgb(93,68,165),		// 169
			Color.FromArgb(93,67,164),		// 170
			Color.FromArgb(93,66,164),		// 171
			Color.FromArgb(93,64,163),		// 172
			Color.FromArgb(93,63,162),		// 173
			Color.FromArgb(93,62,161),		// 174
			Color.FromArgb(92,61,160),		// 175
			Color.FromArgb(92,60,159),		// 176
			Color.FromArgb(92,58,158),		// 177
			Color.FromArgb(92,57,157),		// 178
			Color.FromArgb(92,56,156),		// 179
			Color.FromArgb(92,55,155),		// 180
			Color.FromArgb(92,53,154),		// 181
			Color.FromArgb(91,52,153),		// 182
			Color.FromArgb(91,51,152),		// 183
			Color.FromArgb(91,50,151),		// 184
			Color.FromArgb(91,49,150),		// 185
			Color.FromArgb(90,48,149),		// 186
			Color.FromArgb(90,46,148),		// 187
			Color.FromArgb(90,45,146),		// 188
			Color.FromArgb(90,44,145),		// 189
			Color.FromArgb(89,43,144),		// 190
			Color.FromArgb(89,42,143),		// 191
			Color.FromArgb(89,41,141),		// 192
			Color.FromArgb(88,40,140),		// 193
			Color.FromArgb(88,39,139),		// 194
			Color.FromArgb(87,37,137),		// 195
			Color.FromArgb(87,36,136),		// 196
			Color.FromArgb(87,35,134),		// 197
			Color.FromArgb(86,34,133),		// 198
			Color.FromArgb(86,33,131),		// 199
			Color.FromArgb(85,33,130),		// 200
			Color.FromArgb(85,32,128),		// 201
			Color.FromArgb(84,31,127),		// 202
			Color.FromArgb(83,30,125),		// 203
			Color.FromArgb(83,29,124),		// 204
			Color.FromArgb(82,28,122),		// 205
			Color.FromArgb(82,27,120),		// 206
			Color.FromArgb(81,27,119),		// 207
			Color.FromArgb(80,26,117),		// 208
			Color.FromArgb(80,25,115),		// 209
			Color.FromArgb(79,25,114),		// 210
			Color.FromArgb(78,24,112),		// 211
			Color.FromArgb(77,23,110),		// 212
			Color.FromArgb(77,23,108),		// 213
			Color.FromArgb(76,22,107),		// 214
			Color.FromArgb(75,22,105),		// 215
			Color.FromArgb(74,21,103),		// 216
			Color.FromArgb(73,21,102),		// 217
			Color.FromArgb(73,21,100),		// 218
			Color.FromArgb(72,20,98),		// 219
			Color.FromArgb(71,20,96),		// 220
			Color.FromArgb(70,19,95),		// 221
			Color.FromArgb(69,19,93),		// 222
			Color.FromArgb(68,19,91),		// 223
			Color.FromArgb(68,18,90),		// 224
			Color.FromArgb(67,18,88),		// 225
			Color.FromArgb(66,18,87),		// 226
			Color.FromArgb(65,18,85),		// 227
			Color.FromArgb(64,17,84),		// 228
			Color.FromArgb(63,17,82),		// 229
			Color.FromArgb(62,17,81),		// 230
			Color.FromArgb(62,17,79),		// 231
			Color.FromArgb(61,17,78),		// 232
			Color.FromArgb(60,17,76),		// 233
			Color.FromArgb(59,17,75),		// 234
			Color.FromArgb(58,16,74),		// 235
			Color.FromArgb(58,16,72),		// 236
			Color.FromArgb(57,16,71),		// 237
			Color.FromArgb(56,16,70),		// 238
			Color.FromArgb(56,16,69),		// 239
			Color.FromArgb(55,16,67),		// 240
			Color.FromArgb(54,16,66),		// 241
			Color.FromArgb(54,16,65),		// 242
			Color.FromArgb(53,16,64),		// 243
			Color.FromArgb(52,16,63),		// 244
			Color.FromArgb(52,17,62),		// 245
			Color.FromArgb(51,17,61),		// 246
			Color.FromArgb(51,17,60),		// 247
			Color.FromArgb(50,17,59),		// 248
			Color.FromArgb(50,17,59),		// 249
			Color.FromArgb(50,17,58),		// 250
			Color.FromArgb(49,18,57),		// 251
			Color.FromArgb(48,18,56),		// 252
			Color.FromArgb(48,19,55),		// 253
			Color.FromArgb(47,19,55),		// 254
			Color.FromArgb(47,20,54),		// 255
		};
	}
}
